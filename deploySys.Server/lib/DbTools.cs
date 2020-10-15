using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Chloe;
using Chloe.Ext;
using Chloe.Ext.Intf;
using deploySys.Model;

namespace deploySys.Server.lib
{
    public abstract class baseDbTools
    {
        protected static int _cacheCount = 500;
        protected static int _cacheExpireSeconds = 300;
        protected static bool _useDbStateM = false;
        protected static bool _useTrackEntity = true;
        protected static bool _canInterceptor = false;
        protected virtual string sqlConnTmp { get; set; }
           protected virtual string createDbSqlTmp { get; set; }
           protected virtual string createUserSqlTmp { get; set; }
        protected virtual string grantUserSqlTmp { get; set; }
        protected virtual string ConnectionString { get; set; }
         protected virtual string SpaceFileName { get; set; }
        
        public static baseDbTools getInstance(string DbType, string Ip, string port,string rootUser,string rootPassword)
        {
            baseDbTools bdt=null;
            if (string.Equals(DbType, "sqlite", StringComparison.CurrentCultureIgnoreCase))
            {
                
            }
            else if (string.Equals(DbType, "sqlserver", StringComparison.CurrentCultureIgnoreCase))
            {
               
            }
            else if (string.Equals(DbType, "mysql", StringComparison.CurrentCultureIgnoreCase))
            {
                bdt = new mySqlDbTools();
                bdt.ConnectionString = bdt.getSqlConnectStr(Ip, port, rootUser, rootPassword);
            }
            else if (string.Equals(DbType, "oracle", StringComparison.CurrentCultureIgnoreCase))
            {
                
            }
            else if (string.Equals(DbType, "pgsql", StringComparison.CurrentCultureIgnoreCase))
            {
              
            }
            return bdt;
        }
        protected virtual IContextSpace currDbContext { get; set; }
        protected Assembly getObjectSpaceAssembly()
        {
            Assembly myAssembly = null;
#if NETCORE
            var fullSpaceFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SpaceFileName);
            if (File.Exists(fullSpaceFileName))
                myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(fullSpaceFileName);
            else
            {
                string fn = Path.GetFileNameWithoutExtension(fullSpaceFileName);
                var assems = Assembly.GetEntryAssembly().GetReferencedAssemblies();
                var tmp = (from x in assems where x.Name.Contains(fn) select x).FirstOrDefault();
                if (tmp == null)
                    throw new Exception("找不到spacedll");
                myAssembly = AssemblyLoadContext.Default.LoadFromAssemblyName(tmp);
            }

#endif
#if NETFX
            if (File.Exists(SpaceFileName))
            myAssembly=System.Reflection.Assembly.LoadFile(SpaceFileName);
            
#endif
            if (myAssembly == null)
                throw new FileNotFoundException("找不到ObjectSpace包");
            return myAssembly;
        }
        protected IContextSpace CreatePgSqlContext()
        {
            var myAssembly = getObjectSpaceAssembly();
            var myType = myAssembly.GetType("Chloe.ObjectSpace.PostgreSqlContextSpace");
            var cs = Activator.CreateInstance(myType, new object[] { ConnectionString, _cacheCount, _cacheExpireSeconds,  _useTrackEntity }) as IContextSpace;

            return cs;
        }
        protected IContextSpace CreateSqlServerContext()
        {
            var myAssembly = getObjectSpaceAssembly();
            var myType = myAssembly.GetType("Chloe.ObjectSpace.MssqlContextSpace");
            var cs = Activator.CreateInstance(myType, new object[] { ConnectionString, _cacheCount, _cacheExpireSeconds,  _useTrackEntity }) as IContextSpace;

            return cs;
        }
        protected IContextSpace CreateMySqlContext()
        {
            var myAssembly = getObjectSpaceAssembly();
            var myType = myAssembly.GetType("Chloe.ObjectSpace.MysqlContextSpace");
            var cs = Activator.CreateInstance(myType, new object[] { ConnectionString, _cacheCount, _cacheExpireSeconds,null,false }) as IContextSpace;

            return cs;

        }
        protected IContextSpace CreateOracleContext()
        {
            var myAssembly = getObjectSpaceAssembly();
            var myType = myAssembly.GetType("Chloe.ObjectSpace.OracleContextSpace");
            var cs = Activator.CreateInstance(myType, new object[] { ConnectionString, _cacheCount, _cacheExpireSeconds,  _useTrackEntity }) as IContextSpace;
            return cs;
        }
        protected IContextSpace CreateSQLiteContext()
        {
            var myAssembly = getObjectSpaceAssembly();
            var myType = myAssembly.GetType("Chloe.ObjectSpace.SqlliteContextSpace");
            var cs = Activator.CreateInstance(myType, new object[] { ConnectionString, _cacheCount, _cacheExpireSeconds,  _useTrackEntity }) as IContextSpace;


            return cs;
        }
        public abstract void createDbWithUser(string dbname,string ecoder,string user,string password);
        public  string getSqlConnectStr( string Ip, string port,string rootUser,string rootPassword)
        {
            return string.Format(sqlConnTmp, Ip, port, rootUser, rootPassword);
        }

    }
    public class mySqlDbTools : baseDbTools
    { 
           protected override string sqlConnTmp { get; set; }
        protected override string ConnectionString { get; set; }
        protected override string SpaceFileName { get; set; }
        internal mySqlDbTools()
        {
            sqlConnTmp = "server={0};Port={1};database=mysql;User Id={2};password={3}; Charset=utf8;Connect Timeout=500;SslMode=None";
            createDbSqlTmp = "CREATE DATABASE IF NOT EXISTS {0} DEFAULT CHARACTER SET {1}; ";
            createUserSqlTmp = "CREATE USER '{0}'@'%' IDENTIFIED BY '{1}';";
            grantUserSqlTmp = " GRANT all privileges ON {0}.* to '{1}'@'%'";
            SpaceFileName = "Chloe.MySql.dll";
        }
        public override void createDbWithUser(string dbname,string ecoder,string user,string password)
        {
            try
            {
                var objectSpace = CreateMySqlContext();
                var session = objectSpace.getDbContext().Session;
                var sql = string.Format(createDbSqlTmp, dbname, ecoder);
                session.ExecuteNonQuery(sql);
                sql= string.Format(createUserSqlTmp, user, password);
                session.ExecuteNonQuery(sql);
                sql= string.Format(grantUserSqlTmp, dbname, user);
                 session.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                var message = FrmLib.Extend.tools_static.getExceptionMessage(ex);
                throw new Exception(message);
              
            }

        }
    }

}
