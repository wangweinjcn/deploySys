using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{

    using Newtonsoft.Json.Linq;
        
    public class treeNode
    {
        public treeNode parent { get; set; }
        public string Id { get; set; }
        public int SortCode { get; set; }
        public JToken data { get; set; }
        public List<treeNode> ChildNodes;
        public bool IsMenu { get; set; }
        public bool IsExpand { get; set; }
        public bool IsPublic { get; set; }
        public string OpenTarget { get; set; }
        public JToken toVtableJson()
        {
            JObject jobj = new JObject();
            //foreach (var key in (data as JObject))
            //{
            //    jobj.Add(key.Key, key.Value);

            //}
            jobj.Add("title",
               FrmLib.Extend.tools_static.getJobjectValue<string>(data as JObject, "Name"));

            if (IsMenu)
            { jobj.Add("icon",
                   FrmLib.Extend.tools_static.getJobjectValue<string>(data as JObject, "Icon"));
                jobj.Add("index",
                    FrmLib.Extend.tools_static.getJobjectValue<string>(data as JObject, "Id"));

            }
            else
            {
                jobj.Add("index",
                FrmLib.Extend.tools_static.getJobjectValue<string>(data as JObject, "Key"));
                jobj.Add("UrlAddress",
               FrmLib.Extend.tools_static.getJobjectValue<string>(data as JObject, "UrlAddress"));

            }


            JArray jarr = new JArray();
            foreach (var obj in ChildNodes)
            {
                jarr.Add(obj.toVtableJson());
            }
            jobj.Add("subs", jarr);
            return jobj;
        }
        public JToken tojson()
        {
            JObject jobj = new JObject();
            foreach (var key in (data as JObject))
            {
                jobj.Add(key.Key, key.Value);

            }
            jobj.Add("IsMenu", IsMenu);
            jobj.Add("IsExpand", IsExpand);
            jobj.Add("IsPublic", IsPublic);
            jobj.Add("OpenTarget", OpenTarget);

            JArray jarr = new JArray();
            foreach (var obj in ChildNodes)
            {
                jarr.Add(obj.tojson());
            }
            jobj.Add("ChildNodes", jarr);
            return jobj;
        }
        public treeNode getChildNod(string _id)
        {
            foreach (var obj in this.ChildNodes)
            {
                if (obj.Id == _id)
                    return obj;
                if (obj.ChildNodes.Count == 0)
                {
                    return null;
                }
                else
                {
                    var x = obj.getChildNod(_id);
                    if (x != null)
                        return x;
                }
            }
            return null;
        }
        public treeNode(string id, int sort, JToken _data, treeNode parentid = null,bool isgroup=true)
        {
            this.Id = id;
            this.SortCode = sort;
            this.data = _data;
            this.parent = parentid;
            IsPublic = false;
            if (isgroup)
            {
                IsMenu = false;
                IsExpand = true;
                OpenTarget = "expand";
            }
            else
            {
                IsMenu = true;
                IsExpand = false;
                OpenTarget = "iframe";

            }
            ChildNodes = new List<treeNode>();
        }
        public treeNode appendNewNode(string id, int sort, JToken _data,bool isgroup)
        {
            treeNode node = new treeNode(id, sort, _data, this,isgroup);
            int pos = -1;
            foreach (var obj in ChildNodes)
            {
                if (obj.SortCode < sort)
                    continue;
                else
                {
                    pos = ChildNodes.IndexOf(obj);
                    break;
                }
            }
            if (pos > -1)
            {
                ChildNodes.Insert(pos, node);
            }
            else
                ChildNodes.Add(node);
            return node;

        }
    }
    public  class TreeClass
    {
        treeNode _rootnode;
        public TreeClass()
        {
            _rootnode = new treeNode(null, -1, null);
        }
        public treeNode rootNode { get { return _rootnode; } }
       
        public treeNode insertChild(string id, int sort, JToken _data,bool isgroup,treeNode parentnode=null )
        {
            treeNode pnode;
            if (parentnode == null)
                pnode = _rootnode;
            else
                pnode = parentnode;
          return  pnode.appendNewNode(id, sort, _data,isgroup);

        }
        public treeNode getChildNode(string id, treeNode parentnode = null)
        {
            treeNode pnode;
            if (parentnode == null)
                pnode = _rootnode;
            else
                pnode = parentnode;
            return pnode.getChildNod(id);

        }
        public JArray toVtableJson()
        {
            JArray jarr = new JArray();
            foreach (var obj in _rootnode.ChildNodes)
            {
                jarr.Add(obj.toVtableJson());
            }
            return jarr;
        }
        public JArray tojson()
        {

            JArray jarr = new JArray();
            foreach (var obj in _rootnode.ChildNodes)
            {
                jarr.Add(obj.tojson());
            }
            return jarr;
        }
        public void test()
        {
           
        }
    }
}
