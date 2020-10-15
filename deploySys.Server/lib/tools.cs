using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace deploySys.Server.lib
{
    public class TokenListCache
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxValidSeconds">最长有效时间（单位秒）</param>
        /// <param name="timerCheckDuring">定时器检查时间（单位秒）</param>
        public TokenListCache(int maxValidSeconds = 600, int timerCheckDuring = 30)
        {
            lock (_instanceLock)
            {
                if (_instance != null)
                    return;
                else
                {
                    _lockObj = new object();
                    _validTS = TimeSpan.FromSeconds(maxValidSeconds);
                    _timer = new Timer(timerCheckDuring * 1000) { AutoReset = true };
                    _timer.Elapsed += pTimer_Elapsed;
                    _instance = this;
                    _timer.Start();
                }
            }

        }

        private static object _instanceLock = new object();
        private static TokenListCache _instance = null;
        public static TokenListCache getInstance()
        {
            lock (_instanceLock)
            {
                if (_instance != null)
                    return _instance;
                else
                    throw new NotImplementedException("没有初始化实例");
            }
        }
        public IList<string> getOnlines()
        {
            return (from x in _listToken select x.userId).ToList(); ;
        }
        private object _lockObj;
        private bool nowChecking = false;
        public int getOnlineCount()
        {
            return _listToken.Count();
        }
        private void pTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_lockObj)
            {
                if (nowChecking)
                    return;
                nowChecking = true;
            }
            try
            {
                var list = (from x in _listToken where DateTime.Compare(DateTime.Now, x.expireDt) > 0 select x).ToList();
                foreach (var obj in list)
                {
                    _listToken.Remove(obj);
                    oneTokenData value;
                    _dicToken.TryRemove(obj.userId, out value);
                }
            }
            finally
            {
                nowChecking = false;
            }
        }
        public string getToken(string userid)
        {
            if (_dicToken.ContainsKey(userid))
                return _dicToken[userid].token;
            else
                return null;
        }
        public void removeToken(string userid)
        {
            if (!_dicToken.ContainsKey(userid))
                return;
            lock (this)
            {
                if (!_dicToken.ContainsKey(userid))
                    return;
                var obj = _dicToken[userid];
                oneTokenData value;
                _dicToken.Remove(userid, out value);
                _listToken.Remove(value);
            }
        }
        public bool addToken(string userid, string token)
        {
            if (_dicToken.ContainsKey(userid))
                return true;
            else
            {
                lock (this)
                {
                    if (_dicToken.ContainsKey(userid))
                        return true;
                    else
                    {
                        var obj = new oneTokenData(userid, token);
                        _dicToken.AddOrUpdate(userid, obj, (a, b) => b);
                        _listToken.Add(obj);
                        return true;
                    }
                }
            }
        }
        static TimeSpan _validTS;
        private readonly Timer _timer;
        internal class oneTokenData
        {
            public DateTime expireDt;
            public string userId;
            public string token;
            public oneTokenData(string userid, string token)
            {
                this.expireDt = DateTime.Now.Add(TokenListCache._validTS);
                userId = userid;
                this.token = token;
            }
        }
        List<oneTokenData> _listToken = new List<oneTokenData>();
        ConcurrentDictionary<string, oneTokenData> _dicToken = new ConcurrentDictionary<string, oneTokenData>();

    }
    public class ConcurrentUseListDic<T>
    {
        /// <summary>
        /// 数据包裹
        /// </summary>
        internal class dataPack
        {
            public string key;
            public T data;
            public object props;
            public int count;


            public dataPack(T value, object p)
            {
                data = value;
                props = p;
                count = 0;
            }

        }
        private const long MemoryCacheMax = 4294967296;//4G 最大允许使用内存
        private int maxLength;

        private IList<dataPack> datalist;
        private ConcurrentDictionary<string, dataPack> dict;
        /// <summary>
        /// 启动时间
        /// </summary>
        public DateTime startdt { private set; get; }
        /// <summary>
        /// 请求个数
        /// </summary>
        public long getCount { private set; get; }
        /// <summary>
        /// 换出个数
        /// </summary>
        public long removeCount { private set; get; }
        /// <summary>
        /// 缓存对象个数
        /// </summary>
        public long cacheObjectCount { get { return dict.Keys.Count(); } }
        /// <summary>
        /// 缓存数据大小
        /// </summary>
        public long cacheSizeMemory { private set; get; }
        /// <summary>
        /// 初始化次数
        /// </summary>
        public long reInitCount { private set; get; }
        public ConcurrentUseListDic(int maxCount)
        {
            maxLength = maxCount;
            datalist = new List<dataPack>();
            dict = new ConcurrentDictionary<string, dataPack>();
            getCount = 0;
            removeCount = 0;
            cacheSizeMemory = 0;
            reInitCount = 0;
            startdt = DateTime.Now;
        }
        public bool addOne(string key, T data)
        {
            return addOne(key, data, null);
        }
        public void removeOne(string key)
        {
            if (!dict.ContainsKey(key))
                return;
            dataPack oldone;
            dict.TryRemove(key, out oldone);

            if (datalist.Contains(oldone))
                datalist.Remove(oldone);


        }
        public bool addOne(string key, T data, Object props)
        {
            if (dict.ContainsKey(key))
                return false;
            lock (this)
            {
                if (dict.ContainsKey(key))
                    return false;
                if (dict.Keys.Count >= maxLength || cacheSizeMemory >= MemoryCacheMax)
                {
                    try
                    {
                        string removekey;
                        dataPack removeObj = (from x in datalist orderby x.count select x).FirstOrDefault();

                        if (removeObj == null)
                            return false;
                        else
                        {
                            datalist.Remove(removeObj);
                            if (!dict.ContainsKey(removeObj.key))
                            {
                                throw new Exception("dict not contain removekey");
                            }
                            else
                            {
                                dataPack value;
                                if (!dict.TryRemove(removeObj.key, out value))
                                    throw new Exception("dict remove key error");
                                removeCount++;
                                if (typeof(T) == typeof(byte[]))
                                {
                                    cacheSizeMemory = cacheSizeMemory - (value.data as byte[]).Length;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        datalist = new List<dataPack>();
                        dict = new ConcurrentDictionary<string, dataPack>();
                        getCount = 0;
                        removeCount = 0;
                        cacheSizeMemory = 0;
                        reInitCount++;
                    }
                }

                var newobj = new dataPack(data, props);
                newobj.key = key;
                datalist.Add(newobj);
                dict.AddOrUpdate(key, newobj, (a, b) => b);
                if (typeof(T) == typeof(byte[]))
                {
                    cacheSizeMemory = cacheSizeMemory + (data as byte[]).Length;
                }
            }

            return true;
        }
        public object cacheInfo()
        {
            return new { this.maxLength, this.getCount, this.reInitCount, this.cacheObjectCount, this.cacheSizeMemory, this.startdt, this.removeCount };
        }
        public bool ContainsKey(string key)
        {
            return dict.ContainsKey(key);
        }
        public T getValue(string key)
        {

            if (dict.ContainsKey(key))
            {
                lock (this)
                {
                    if (!dict.ContainsKey(key))
                        return default(T);
                    getCount++;
                    var dp = dict[key];
                    dp.count++;

                    return dp.data;
                }
            }
            else
                return default(T);
        }
        public object getProps(string key)
        {
            if (dict.ContainsKey(key))
            {
                lock (this)
                {
                    if (!dict.ContainsKey(key))
                        return null;
                    return dict[key].props;
                }
            }
            else
                return null;
        }
        public void setLength(int maxCount)
        {
            maxLength = maxCount;
        }
        public int getlength()
        {
            return dict.Keys.Count();
        }
    }


}
