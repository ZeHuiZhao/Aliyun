using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T4
{
    public abstract class Page:System.Web.UI.Page
    {
        protected override bool SupportAutoEvents
        {
            get
            {
                return false;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            var cmd = this.Request["cmd"];
            if (!string.IsNullOrEmpty(cmd))
            {
                //cmd = cmd.ToLower().Trim();
                //if (!DictMethod.ContainsKey(cmd))
                //    throw new ArgumentException("未识别的cmd");
                //DictMethod[cmd].Invoke(this, null);
                this.GetType().InvokeMember(cmd, System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase, null, this, null);
                this.Response.End();
            }
            this.OnLoad();
            
        }

        protected abstract void OnLoad();

        
    }
}