using   Chloe.Entity;  
using   Chloe.Annotations;  
using   Chloe.Ext; 
using   Chloe.Ext.Aop; 
using   Chloe.Ext.Attribute;
using   Chloe.Descriptors; 
using   Chloe.Ext.Intf; 
using   System; 
using   System.Collections.Generic; 
using   System.Linq;
using   Newtonsoft.Json;
using   MessagePack;
using  System.ComponentModel.DataAnnotations;
#if NETFX
using System.Web.ModelBinding;
#endif
#if NETCORE
using  Microsoft.AspNetCore.Mvc.ModelBinding;
#endif
namespace Application.Model.Base
{
      using   Application.Model.Workflow;
      using   Chloe.Ext.stateMachine;
      using   FrmLib.Encrypt;
      using   FrmLib.Extend;
      using   Newtonsoft.Json.Linq;
      using   System.IO;
      using   System.Security.Claims;
      using   System.Security.Principal;
        ///
        ///<summary></summary>
public   partial class SysFunc : _baseObject{
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="1c483f60-52fb-4d16-9295-9dcd8c934168")]
        public  SysUser  AddUser(  String  loginid,  String  mobile,  String  name,  String  password,  String  email,  Int32  sex,  Int32  logintype,  String  otherLoginId,  String  sitekey)

{
            var lowLoginId = loginid.ToLower();
            var lowEmail = email==null?"": email.ToLower();
            var q = GetSpace().SpaceQuery<SysUser>().Where(a=>a.AppSiteKey==sitekey).Where(a =>
               a.LoginId.ToLower() == lowLoginId
               ||(lowEmail!="" &&  a.Email.ToLower() == lowEmail)
               || a.Mobile == mobile);
            var count =q.Count();
            //FrmLib.Log.commLoger.devLoger.Debug("add user debug," + lowEmail + "," + lowLoginId + "," + mobile+","+count.ToString());
            //FrmLib.Log.commLoger.devLoger.Debug("sql:" + q.ToString());
            if (count > 0)
                throw new Exception("已经存在同名或同手机号码的账号");
            SysUser user = new SysUser(GetSpace());
            user.LoginId = loginid;
            user.Mobile = mobile;
            user.UserName = name;
            user.secretKey = System.Guid.NewGuid().ToString().Substring(0, 16);
            user.Password = AesTools.AesEncrypt(password.ToUpper(), user.secretKey);
            user.loginType = logintype;
            user.sex = sex;
            user.Email = email;
            user.OtherLoginId = otherLoginId;
            user.AppSiteKey = sitekey;
            return user;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="1d0d5421-f9c2-4fd2-b2dd-bdd78027712b")]
        public  Byte[]  GetFile(  String  guid)

{
            var fvd = GetSpace().SpaceQuery<FileVDir>().Where(a => a.Guid == guid).FirstOrDefault();
            if (fvd == null)
                return null;
            var fn = Path.Combine(fvd.Ass_PhysicalPath.getFullPath(), fvd.Guid);
            var content = File.ReadAllBytes(fn);
            return content;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="266dbd32-6209-4329-acea-24d5144d8fc7")]
        public  SysUser  getCurrentUser()

{
            SysUser curr = null;
            string userid = "";
            ClaimsPrincipal iprincipal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            FrmLib.Log.commLoger.devLoger.Debug("iprincipal:"+(iprincipal==null).ToString());
            if (iprincipal != null)
            {
                ClaimsIdentity ci = (iprincipal.Identity as ClaimsIdentity);
                var clm = (from x in ci.Claims where x.Type == "UserId" select x).FirstOrDefault();
                userid = clm == null ? "" : clm.Value;
                FrmLib.Log.commLoger.devLoger.Debug("iprincipal user id:" + userid);
                var space = GetSpace();
                FrmLib.Log.commLoger.devLoger.Debug("iprincipal space :" + (space==null).ToString());
                if (space != null)
                {
                    curr = space.ObjectForId<SysUser>(userid);
                     FrmLib.Log.commLoger.devLoger.Debug("iprincipal curr :" + (curr==null).ToString());
                }
               
            }
            return curr;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="2f017a64-cdef-44c8-b3a2-6808a7d2822c")]
        public  MenuGroup  AddMenuGroup(  String  name,  MenuGroup  parent)

{
            MenuGroup menuGroup = new MenuGroup(GetSpace());
            menuGroup.Name = name;
            menuGroup.Ass_Parent = parent;
            return menuGroup;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="34e84879-aebe-4967-a8f6-9703525c3041")]
        public  PhysicalPath  getPhysicPath()

{
            var rootpd = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Ass_LocalPath_parent == null || a.Ass_LocalPath_parent_Id < 0).FirstOrDefault();
            var ppath = rootpd.Name;
            if (!Directory.Exists(ppath))
            {
                Directory.CreateDirectory(ppath);
            }
            var str1 = "y" + DateTime.Now.ToString("yyyy");
            PhysicalPath phyear = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str1)).OrderByDesc(a => a.Id).FirstOrDefault();
            if (phyear == null)
            {
                lock (_lockObj)
                {
                     phyear = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str1)).OrderByDesc(a => a.Id).FirstOrDefault();
                    if (phyear == null)
                    {
                        phyear = new PhysicalPath(GetSpace());
                        phyear.Name = str1;
                        phyear.Ass_LocalPath_parent = rootpd;
                        GetSpace().updateOneDirtyObject(phyear);
                    }
                }
            }
            var pathyear = Path.Combine(ppath, str1);
            if (!Directory.Exists(pathyear))
            {
                Directory.CreateDirectory(pathyear);
            }
            var dircount = Directory.GetDirectories(pathyear);
            if (dircount.Count() > 1000)
            {
                lock (_lockObj)
                {
                    phyear = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str1)).OrderByDesc(a => a.Id).FirstOrDefault();
                    pathyear = Path.Combine(ppath, str1);
                    dircount = Directory.GetDirectories(pathyear);
                    if (dircount.Count() > 1000)
                    {
                        var yearcount = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str1)).Count();
                        str1 = str1 + "_" + (yearcount + 1).ToString();
                        phyear = new PhysicalPath(GetSpace());
                        phyear.Name = str1;
                        phyear.Ass_LocalPath_parent = rootpd;
                        pathyear = Path.Combine(ppath, str1);
                        if (!Directory.Exists(pathyear))
                        {
                            Directory.CreateDirectory(pathyear);
                        }
                        GetSpace().updateOneDirtyObject(phyear);
                    }
                }
            }



            var str2 = "d" + DateTime.Now.ToString("MMdd");
            PhysicalPath phday = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith( str2) && a.Ass_LocalPath_parent==phyear).OrderByDesc(a=>a.Id).FirstOrDefault();
            if (phday == null)
            {
                lock (_lockObj)
                {
                    phday = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str2)  && a.Ass_LocalPath_parent==phyear).OrderByDesc(a => a.Id).FirstOrDefault();
                    if (phday == null)
                    {
                        phday = new PhysicalPath(GetSpace());
                        phday.Name = str2;
                        phday.Ass_LocalPath_parent = phyear;
                        GetSpace().updateOneDirtyObject(phday);
                    }
                }
            }
            var currDayPath = Path.Combine(pathyear, phday.Name);
            if (!Directory.Exists(currDayPath))
            {
                Directory.CreateDirectory(currDayPath);
            }
            var fcount = Directory.GetFiles(currDayPath);
            if (fcount.Count() > 1000) //单个目录文件上限为1000
            {
                lock (_lockObj)
                {
                    phday = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str2)).OrderByDesc(a => a.Id).FirstOrDefault();
                    currDayPath = Path.Combine(pathyear, phday.Name);
                    if (!Directory.Exists(currDayPath))
                    {
                        Directory.CreateDirectory(currDayPath);
                    }
                    fcount = Directory.GetFiles(currDayPath);
                    if (fcount.Count() > 1000) //单个目录文件上限为1000
                    {
                        var phDayCount = GetSpace().SpaceQuery<PhysicalPath>().Where(a => a.Name.StartsWith(str2)).Count();
                        str2 = str2 + "_" + (phDayCount + 1).ToString();
                        phday = new PhysicalPath(GetSpace());
                        phday.Name = str2;
                        phday.Ass_LocalPath_parent = phyear;
                        currDayPath = Path.Combine(pathyear, str2);
                        GetSpace().updateOneDirtyObject(phday);
                        if (!Directory.Exists(currDayPath))
                        {
                            Directory.CreateDirectory(currDayPath);
                        }
                    }
                }
            }
            return phday;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="3ec85478-e17d-4798-bc14-21113917e2d1")]
        public  SysUser  getSysUser(  String  loginId,  String  siteKey)

{
            var lowLoginId = loginId.ToLower();
            var lowsitekey = siteKey.Trim().ToLower();
            var q = GetSpace().SpaceQuery<SysUser>().Where(a => a.AppSiteKey.Trim().ToLower() == lowsitekey).Where(a =>
                   a.LoginId.ToLower() == lowLoginId
                   || ( a.Email.ToLower() == lowLoginId)
                   || a.Mobile == loginId);
            return q.FirstOrDefault();
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="4900d8dc-c2f1-4df1-bb09-734422374a1f")]
        public  SysRole  AddSysRole(  String  name,  String  roleId,  Boolean  isadmin=false)

{

            SysRole r = new SysRole(GetSpace());
            r.Name = name;
            r.RoleID = roleId;
            r.IsAdmin = isadmin;
            return r;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="4dea44e8-df09-4ef2-a562-f01ea0f86c8c")]
        public  String  getParamValue(  String  key)

{
            var param = GetSpace().SpaceQuery<SysParams>().Where(x => x.Key == key).FirstOrDefault();
            if (param == null)
                return null;
            if (string.IsNullOrEmpty(param.Value))
                return param.largeValue;
            else
                return param.Value;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="6871b71a-98bb-46b0-89ed-7ee3536c90f7")]
        public  Int32  GetNextValue(  String  key,  String  paramValue)

{
            IDClass idc = GetSpace().SpaceQuery<IDClass>().Where(a => a.Key == key && a.ParmasValue == paramValue).FirstOrDefault();
            if (idc == null)
            {
                try
                {
                    idc = new IDClass(GetSpace());
                    idc.Key = key;
                    idc.ParmasValue = paramValue;
                    idc.IDValue = 1;
                    GetSpace().UpdateAllDirtyObjects(true);
                }
                catch (Exception ex)
                {
                    idc = GetSpace().SpaceQuery<IDClass>().Where(a => a.Key == key && a.ParmasValue == paramValue).FirstOrDefault();

                }
            }

            if (idc == null)
                throw new Exception("IdClass get error");
            bool isok=false;
            int res=-1;
            while (!isok)
            {
                res = idc.IDValue+1;
                try
                {
                    GetSpace().getDbContext().Update<IDClass>(a => a.Id == idc.Id && a.IDValue == idc.IDValue, a => new IDClass { IDValue = res });
                    isok = true;
                }
                catch (Exception)
                {
                    idc= GetSpace().SpaceQuery<IDClass>().Where(a => a.Key == key && a.ParmasValue == paramValue).FirstOrDefault();
                }
            }
            return res;
        }
       ///<summary>
       ///添加地区
       ///</summary>
        [UmlElement(Id="74e84067-9ba9-4acf-9319-9cd418b44aae")]
        public  Areas  AddAreas(  String  key,  String  name,  Int32  atype,  Areas  pArea)

{
            throw new NotImplementedException();

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="7b9688c2-3401-451e-8355-d7b0645f7a19")]
        public   void   ImportWfScheme(  String  cname,  String  fname,  String  schemeKey,  String  memo,  String  wfJson)

{
            JObject jobj = JObject.Parse(wfJson);
            IList<wfState> statetmplist = JsonConvert.DeserializeObject<List<wfState>>(jobj["allstate"].ToString());
            IList<wfAction> actiontmplist = JsonConvert.DeserializeObject<List<wfAction>>(jobj["allaction"].ToString());
            var dbstatelist = GetSpace().SpaceQuery<wfDbState>().ToList();
            var dbactionlist = GetSpace().SpaceQuery<wfDbAction>().ToList();
            var dbregionlist = GetSpace().SpaceQuery<wfDbregion>().ToList();
            WfDbStateScheme scheme =GetSpace().SpaceQuery<WfDbStateScheme>().Where(a=>a.key==schemeKey).FirstOrDefault();
            if (scheme == null)
                scheme = new WfDbStateScheme(GetSpace());
            scheme.key = schemeKey;
            scheme.ClassName = cname;
            scheme.FieldName = fname;
            scheme.Name = memo;
            wfDbregion rootregion = (from x in dbregionlist
                                     where x.Ass_WfDbStateScheme == scheme && x.name == "RootRegion"
                                     select x).FirstOrDefault();
            if (rootregion == null)
            {
                rootregion = new wfDbregion(GetSpace());
                rootregion.name = "RootRegion";
                rootregion.Ass_WfDbStateScheme = scheme;
                rootregion.GuId = System.Guid.NewGuid().ToString();
                rootregion.fieldname = fname;
                rootregion.isroot = true;
                dbregionlist.Add(rootregion);
            }
           
            foreach (var oneobj in statetmplist)
            {
                // addState(oneobj.GuId, oneobj.name, oneobj.RegionName, rfname, oneobj.isRegionState);
                if (oneobj.isRegionState)
                {

                    createDbRegion(dbregionlist, statetmplist, oneobj, scheme,dbstatelist);
                }
                else
                {
                    wfDbState dbstate = (from x in dbstatelist where x.GuId == oneobj.GuId select x).FirstOrDefault();
                    if (dbstate == null)
                    {
                        dbstate = new wfDbState(GetSpace());
                        dbstatelist.Add(dbstate);
                    }
                    wfDbregion stateBelongtoregion = (from x in dbregionlist
                                            where x.Ass_WfDbStateScheme == scheme
                                                && x.name == oneobj.RegionName
                                            select x).FirstOrDefault();
                    if(stateBelongtoregion==null)
                    {
                        var wfregionstate = (from x in statetmplist where x.isRegionState 
                                        && x.name == oneobj.RegionName select x).FirstOrDefault();
                        if (wfregionstate == null)
                            throw new Exception("找不到关联的region");
                        stateBelongtoregion = createDbRegion(dbregionlist, statetmplist, wfregionstate, scheme,dbstatelist);

                    }
                    dbstate.ApplyNewFromWfState(oneobj);
                    dbstate.fieldName = scheme.FieldName;
                    dbstate.Ass_WfDbStateScheme = scheme;
                    dbstate.Ass_wfDbregion = stateBelongtoregion;
                }
            }
            GetSpace().UpdateAllDirtyObjects();
            foreach (var oneact in actiontmplist)
            {
                wfDbAction onedbaction = (from x in dbactionlist
                                          where  x.GuId == oneact.GuId
                                          select x).FirstOrDefault();

                wfDbregion oneregion = (from x in dbregionlist
                                        where x.Ass_WfDbStateScheme == scheme
                                            && x.name == oneact.regionName
                                        select x).FirstOrDefault();
                if (oneregion == null)
                    throw new Exception("找不到关联的region");

                if (onedbaction == null)
                {
                    onedbaction = new wfDbAction(GetSpace());
                    dbactionlist.Add(onedbaction);
                    

                }

                onedbaction.ApplyNewFromWfAction(oneact);
                onedbaction.Ass_wfDbregion = oneregion;
                onedbaction.Ass_WfDbStateScheme = scheme;
                wfDbState dbfromstate = (from x in dbstatelist
                                     where  x.GuId == oneact.fromstateID
                                     select x).FirstOrDefault();
                if (dbfromstate != null)
                {
                    onedbaction.Ass_froms=dbfromstate;
                  
                    
                }
                else
                    if(!string.IsNullOrEmpty(oneact.fromstateID))
                         throw new Exception("找不到起始状态");
                wfDbState dbtostate = (from x in dbstatelist
                                       where x.GuId == oneact.tostateID
                                       select x).FirstOrDefault();
                if (dbtostate == null)
                {
                    if (!string.IsNullOrEmpty(oneact.tostateID))
                        throw new Exception("找不到目标状态");
                }
                else
                {
                    onedbaction.Ass_to = dbtostate;
                    onedbaction.tostateID = dbtostate.GuId;
                    onedbaction.tostateName = dbtostate.Name;
                }
                //    addAction(oneact.fieldName, oneact.name, oneact.regionName
                //        , oneact.fromstateID, oneact.tostateID, oneact.startorEnd,
                //        oneact.trigger, oneact.guard, oneact.effect, oneact.GuId, oneact.memo);
                //
            }
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="8e270ead-edae-4d36-9798-9e696aedf7af")]
        public   void   InitParams()

{
            int bitLength = 1024;
            IContextSpace sp = this.GetSpace();
            var z = (from x in this.GetSpace().SpaceQuery<SysParams>() where (x.Value == "1" && x.Key == "sysinit") select x).ToList();
            if (z == null || z.Count == 0)
            {
                try
                {
                    SysParams newobj = new SysParams(sp);
                    newobj.Key = "sysinit";
                    newobj.Value = "1";
                    newobj.Category = "system";
                    FrmLib.Encrypt.RsaBcTools rt = new FrmLib.Encrypt.RsaBcTools(bitLength, FrmLib.Encrypt.enumEngineType.Pkcs1Encoding);

                    newobj = new SysParams(sp);
                    newobj.Key = "privateKey";
                    newobj.Memo = bitLength.ToString();
                    newobj.Value = "refer to Large";
                    newobj.largeValue = Convert.ToBase64String(rt.getPriFileContent());
                    newobj.Category = "system";

                    newobj = new SysParams(sp);
                    newobj.Key = "publicKey";
                    newobj.Memo = bitLength.ToString();
                    newobj.Value = "refer to Large";
                    newobj.largeValue = Convert.ToBase64String(rt.getPubFileContent());
                    newobj.Category = "system";

                    newobj = new SysParams(sp);
                    newobj.Key = "CountPerPage";
                    newobj.Value = "20";
                    newobj.Category = "system";
                    newobj = new SysParams(sp);
                    newobj.Key = "RedisConfJson";
                    newobj.Value = "20";
                    newobj.Category = "system";

                    newobj = new SysParams(sp);
                    newobj.Key = "ExchangeRate";
                    newobj.Value = "7";
                    newobj.Category = "businese";
                    newobj.Memo = "人民币美元汇率（一美元兑换）";
                    newobj = new SysParams(sp);
                    newobj.Key = "IncomeDayMax";
                    newobj.Value = "30";
                    newobj.Category = "businese";
                    newobj.Memo = "收款最长天数";

                    var mg = this.AddMenuGroup("账号管理", null);
                    mg.AddMenu("accountRole", "Role", "/Admin/role/index");
                    var m2 = mg.AddMenu("accountUser", "User", "/Admin/user/index");
                    mg = this.AddMenuGroup("菜单管理", null);
                    var m1 = mg.AddMenu("menuGroup", "group", "/Admin/SysConf/MenuGroupIndex");

                    mg.AddMenu("menu", "menu", "");
                    mg = this.AddMenuGroup("系统参数", null);

                    this.AddMenuGroup("系统管理", null);


                    AddExpressCompany("中通快递", null, null);
                    AddExpressCompany("中铁快运", null, null);
                    AddExpressCompany("宅急送", null, null);
                    AddExpressCompany("增益速递", null, null);
                    AddExpressCompany("韵达快递", null, null);
                    AddExpressCompany("圆通速递", null, null);
                    AddExpressCompany("邮政小包", null, null);
                    AddExpressCompany("邮政国内小包", null, null);
                    AddExpressCompany("新邦物流", null, null);
                    AddExpressCompany("天天快递", null, null);
                    AddExpressCompany("天地华宇", null, null);
                    AddExpressCompany("速尔", null, null);
                    AddExpressCompany("顺丰速运", null, null);
                    AddExpressCompany("申通快递", null, null);
                    AddExpressCompany("全一快递", null, null);
                    AddExpressCompany("全峰快递", null, null);
                    AddExpressCompany("佳吉快运", null, null);
                    AddExpressCompany("凡宇速递", null, null);
                    AddExpressCompany("德邦物流", null, null);
                    AddExpressCompany("德邦快递", null, null);
                    AddExpressCompany("城市100", null, null);
                    AddExpressCompany("百世物流", null, null);
                    AddExpressCompany("百世汇通", null, null);
                    AddExpressCompany("E速宝", null, null);
                    AddExpressCompany("EMS经济快递", null, null);
                    AddExpressCompany("EMS", null, null);
                    GetSpace().UpdateAllDirtyObjects(true);
                   }
                catch (Exception exp)
                {
                    Console.WriteLine("eror:{0}", exp.Message);
                    throw exp;
                }
            }


        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="9514a510-f955-4359-a84f-76a8f4f264dc")]
        public  SysRole  getRoleByeKey(  String  rolekey)

{
            var lowkey = rolekey.Trim().ToLower();
            return GetSpace().SpaceQuery<SysRole>().Where(a => a.RoleID.Trim().ToLower() == lowkey).FirstOrDefault();
 
        }
       ///<summary>
       ///menutype:0 ace菜单；1：vue菜单
       ///</summary>
        [UmlElement(Id="9acdeae9-b576-4ada-b372-e88f1eecfe62")]
        public  JToken  AllMenu(  Int32  menutype=0)

{
            JArray jobj = new JArray();
            TreeClass menulist = new TreeClass();
            var z = GetSpace().SpaceQuery<MenuGroup>()
                .Where(a => a.Ass_Parent_Id == null || a.Ass_Parent_Id<=0).ToList();
            foreach (var obj in z)
            {
                cycleTree(obj, menulist.rootNode);

            }
            switch (menutype)
            {
                case 0:
                    jobj = menulist.tojson();
                    break;
                case 1:
                    jobj = menulist.toVtableJson();
                    break;

            }
           
               
            return jobj;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="a5a2dd28-6097-4737-b932-49d34e151604")]
        public  FileVDir  AddNewFile(  Byte[]  content,  String  filename)

{
            PhysicalPath pp = getPhysicPath();
            FileVDir fvd = new FileVDir(GetSpace());
            fvd.Name = filename;
            fvd.FileSize = content.Count();
            fvd.isFile = true;
            fvd.Version = 1;
            fvd.Guid = Guid.NewGuid().ToString("D");
            fvd.Ass_PhysicalPath = pp;
            var path = pp.getFullPath();
            var fn = Path.Combine(path, fvd.Guid);
            File.WriteAllBytes(fn, content);
            return fvd;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="b4133764-a6c0-48f0-a8ae-5df3f704752e")]
        public  SysUser  CheckLogin(  String  loginID,  String  password,  String  siteKey="")

{
            SysUser loginuser = null;
            var z = GetSpace().SpaceQuery<SysUser>().Where(a=>a.IsEnabled && (a.AppSiteKey==siteKey || a.AppSiteKey=="*" )).Where(a => a.LoginId == loginID
              || a.Mobile == loginID || a.Email == loginID).FirstOrDefault();
            if (z != null)
            {
                string secret = FrmLib.Encrypt.AesTools.AesEncrypt(password.ToUpper(), z.secretKey);
                if (z.Password == secret)
                {
                    loginuser = z;
                    
                }
            }
            return loginuser;
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="bf258800-4bba-4147-aca7-1516df1f6ab0")]
        public  static  Tuple< String,string>  getCurrentUID()

{
            string userid = "", username = "";
            ClaimsPrincipal iprincipal = System.Threading.Thread.CurrentPrincipal as ClaimsPrincipal;
            if (iprincipal != null)
            {
                ClaimsIdentity ci = (iprincipal.Identity as ClaimsIdentity);
                var clm = (from x in ci.Claims where x.Type == "UserId" select x).FirstOrDefault();
                userid = clm == null ? "" : clm.Value;
                clm = (from x in ci.Claims where x.Type == "UserName" select x).FirstOrDefault();
                username = clm == null ? "" : clm.Value;
                return new Tuple<string, string>(userid, username);
            }

            return null;

        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="d7ec4823-0ed3-40fd-a4f2-2d903375e68c")]
        public   void   setParamValue(  String  key,  String  value)

{
           var param= GetSpace().SpaceQuery<SysParams>().Where(x => x.Key == key).FirstOrDefault();
            if (param == null)
                param = new SysParams(GetSpace());
            param.Key = key;
            if (value.Length > 255)
                param.largeValue = value;
            else
                param.Value = value;
            
        }
       ///<summary>
       ///
       ///</summary>
        [UmlElement(Id="f2730def-3e81-4994-8e72-891fe7b83960")]
        public  ExpressCompany  AddExpressCompany(  String  namecn,  String  nameen,  String  url)

{
            ExpressCompany ec = new ExpressCompany(GetSpace());
            ec.NameCn = string.IsNullOrEmpty(namecn)?"": namecn.Trim();
            ec.NameEn = string.IsNullOrEmpty(nameen) ? "" : nameen.Trim();
            ec.Url = string.IsNullOrEmpty(url)? "" : url.Trim();
            return ec;
        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="33abb285-b98a-47ef-a6c1-6c9ea8f475dc_getInstance")]
       public static  SysFunc getInstance(IContextSpace space)
{
            SysFunc instance = null;
            if (space.SingletonDic.ContainsKey(typeof(SysFunc).Name))
                instance = space.SingletonDic[typeof(SysFunc).Name] as SysFunc;
            else
            {
                lock (_lockObj)
                {
                    instance = new SysFunc(space);
                    if (!space.SingletonDic.TryAdd(typeof(SysFunc).Name, instance))
                    {
                        if (space.SingletonDic.ContainsKey(typeof(SysFunc).Name))
                            space.SingletonDic[typeof(SysFunc).Name] = instance;
                    }
                }
            }
            return instance;

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="33abb285-b98a-47ef-a6c1-6c9ea8f475dc_OnConstructer")]
       public SysFunc(IContextSpace space): base(space)
{

        }
       ///
        ///<summary>
       ///</summary>
       [UmlElement(Id="33abb285-b98a-47ef-a6c1-6c9ea8f475dc_OffConstructer")]
       [SerializationConstructor]
       public SysFunc(): this(null)
{

        }
       ///<summary>
       ///
       ///</summary>
private wfDbregion createDbRegion(IList<wfDbregion> dbregionlist,IList<wfState> regionstate,    wfState currstate,WfDbStateScheme scheme,IList<wfDbState> alldbstateList)     
 {
            if (!currstate.isRegionState)
                return null;
            wfDbregion rootregion;
        
            rootregion = (from x in dbregionlist
                              where x.Ass_WfDbStateScheme == scheme
                                  && x.name == currstate.parentRegionName
                              select x).FirstOrDefault();
           
            if (rootregion == null)
            {
                wfState parentstate = (from x in regionstate
                                       where x.name == currstate.parentRegionName
                                        && x.isRegionState
                                       select x).FirstOrDefault();
                if (parentstate == null)
                    throw new Exception(string.Format("can't find the parent state of {0}"
                        ,currstate.parentRegionName));
                rootregion= createDbRegion(dbregionlist, regionstate, parentstate, scheme,alldbstateList);

                
            }
            wfDbregion oneregion = (from x in dbregionlist
                                    where x.Ass_WfDbStateScheme == scheme
                                        && x.name == currstate.name
                                    select x).FirstOrDefault();
            
            if (oneregion == null)
            {
                oneregion = new wfDbregion(GetSpace());

                dbregionlist.Add(oneregion);
            }
            oneregion.name = currstate.name;

            oneregion.GuId = currstate.GuId;
            oneregion.fieldname = scheme.FieldName;
            oneregion.Ass_wfDbregion_parent = rootregion;
            oneregion.Ass_WfDbStateScheme = scheme;
            wfDbState onestate = (from x in alldbstateList where
                                 x.GuId == currstate.GuId && x.IsRegion
                                  select x).FirstOrDefault();
            if (onestate == null)
            {
                onestate = new wfDbState(GetSpace());
                alldbstateList.Add(onestate);
               
            }
            onestate.ApplyNewFromWfState(currstate);
            onestate.fieldName = scheme.FieldName;
            onestate.Ass_WfDbStateScheme = scheme;
            onestate.Ass_wfDbregion = oneregion;
            onestate.IsRegion = true;
            return oneregion;
        }
       ///<summary>
       ///
       ///</summary>
private void cycleTree(MenuGroup mg, treeNode ParentNode)     
 {
            var currNode = ParentNode.appendNewNode(mg.Id.ToString(), mg.SortCode, mg.CsObjToJson(), true);
            if (mg.Ass_Menu != null)
            {
                foreach (var menuobj in mg.Ass_Menu)
                    currNode.appendNewNode("menu_" + menuobj.Id.ToString(), menuobj.SortCode, menuobj.CsObjToJson(), false);
            }
            if (mg.Ass_childrens.Count > 0)
            {
                foreach (var childobj in mg.Ass_childrens)
                {

                    cycleTree(childobj, currNode);
                }
            }
        }


        










































































































































































































































































































































































}
}
