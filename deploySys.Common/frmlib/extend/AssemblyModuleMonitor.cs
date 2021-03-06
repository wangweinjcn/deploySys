﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace FrmLib.Extend
{
    public class MonitorFileInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; private set; }
        /// <summary>
        /// 文件最后修改时间
        /// </summary>
        private DateTime modifyDt;
        /// <summary>
        /// 修改标志
        /// </summary>
        public bool isModify { get; private set; }
        /// <summary>
        /// 新增标志
        /// </summary>
        public bool isNew { get; private set; }
        public bool isconfigfile { get; private set; }
        /// <summary>
        /// 修改前的内容
        /// </summary>
        private byte[] oldContent;
        /// <summary>
        /// 修改后的内容
        /// </summary>
        private byte[] newContent;
        public bool isDelete { get; private set; }
        public MonitorFileInfo(string fname, DateTime modifydt, byte[] filecontent, bool isnew = false)
        {
            fileName = fname;
            newContent = filecontent;
            modifyDt = modifydt;
            isNew = isnew;
            isModify = false;
        }
        public bool haveChange(DateTime modifydt)
        {
            isDelete = false;//因为在比较是否修改，所以肯定没有被删除
            if (DateTime.Compare(modifydt, this.modifyDt)!=0)
            {
                Console.WriteLine("old dt:{0},new dt:{1}", this.modifyDt.Ticks, modifyDt.Ticks);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void setModify(byte[] content, DateTime modifydt)
        {
            oldContent = newContent;
            newContent = content;
            this.modifyDt = modifydt;
            isModify = true;
        }
        public void haveDone()
        {
            isModify = false;
            isNew = false;
            isDelete = false;
        }
        public void setDelete()
        {
            isDelete = true;
        }
        public byte[] getContent()
        {
            return newContent;
        }
        public byte[] getOldContent()
        {
            return oldContent;

        }
    }
    public class FileChangeMonitor
    {
        private Dictionary<string, MonitorFileInfo> asmFileDic;
        private string monitorDir;
        public FileChangeMonitor(string dir,string extendname=".dll")
        {
            asmFileDic = new Dictionary<string, MonitorFileInfo>();
            this.monitorDir = dir;
            string[] filelist = Directory.GetFiles(monitorDir);

            MonitorFileInfo asmFileInfo;
            foreach (var filename in filelist)
            {
                if (string.IsNullOrEmpty(extendname) || (!filename.EndsWith(extendname, StringComparison.OrdinalIgnoreCase)))
                    continue;
                asmFileInfo = new MonitorFileInfo(filename, File.GetLastWriteTime(filename), File.ReadAllBytes(filename), true);
                addAsmmblyFileInfo(asmFileInfo);

            }

        }
        public void addMonitorFile(string filename)
        {
            MonitorFileInfo asmFileInfo = new MonitorFileInfo(filename, File.GetLastWriteTime(filename), File.ReadAllBytes(filename), true);
            addAsmmblyFileInfo(asmFileInfo);
        }

        public void addAsmmblyFileInfo(MonitorFileInfo afi)
        {
            if (!asmFileDic.ContainsKey(afi.fileName))
            {
                asmFileDic.Add(afi.fileName, afi);
            }
        }
        public void clear()
        {
            this.asmFileDic.Clear();
        }
        public List<Assembly> getChangedList()
        {
            List<Assembly> asmlist = new List<Assembly>();
            foreach (var asmFileInfo in this.asmFileDic.Values)
            {
                Assembly ab;
                if (asmFileInfo.isModify || asmFileInfo.isNew)
                {
                    ab = Assembly.Load(asmFileInfo.getContent());
                    asmlist.Add(ab);
                }
            }
            return asmlist;
        }

        public List<Assembly> CheckMoidfy()
        {
            Console.WriteLine("now check {0} change:",monitorDir);
            string[] filelist = Directory.GetFiles(monitorDir);
            foreach (var asfile in this.asmFileDic.Values)
            {
                asfile.setDelete();
            }
            MonitorFileInfo asmFileInfo;
            List<Assembly> deleteAsmlist = new List<Assembly>();
            Assembly assem;
            foreach (var filename in filelist)
            {
                if (filename.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                {
                    if (this.asmFileDic.ContainsKey(filename))
                    {
                        asmFileInfo = asmFileDic[filename];
                        if (asmFileInfo.haveChange(File.GetLastWriteTime(filename)))
                        {
                            Console.WriteLine("{0} is changed, now reload", filename);
                            asmFileInfo.setModify(File.ReadAllBytes(filename), File.GetLastWriteTime(filename));
                            assem = Assembly.Load(asmFileInfo.getOldContent());
                            deleteAsmlist.Add(assem);
                        }

                    }
                    else
                    {
                        Console.WriteLine("{0} is added, now reload", filename);
                        asmFileInfo = new MonitorFileInfo(filename, File.GetLastWriteTime(filename), File.ReadAllBytes(filename), true);
                        addAsmmblyFileInfo(asmFileInfo);
                    }
                }
            }
            foreach (var deletobj in this.asmFileDic.Values)
            {
                if (deletobj.isDelete)
                {
                    assem = Assembly.Load(deletobj.getContent());
                    deleteAsmlist.Add(assem);
                }
            }

            return deleteAsmlist;
        }
        /// <summary>
        /// 修改标志和新增标志复位，置为false;调用haveDone以后，就不能正确获取修改和删除的列表了。
        /// </summary>
        public void haveDone()
        {
            foreach (var asmfileinfo in this.asmFileDic.Values)
            {
                if (asmfileinfo.isDelete)
                {
                    this.asmFileDic.Remove(asmfileinfo.fileName);
                    continue;
                }
                asmfileinfo.haveDone();
            }

        }
    }

}
