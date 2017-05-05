using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WMS_v1._0.Util
{
    public class PageUtil
    {
        public static void showToast(Page page, string message)
        {
            string showtoast = "<script type='text/javascript'>function createModel() { window.clearTimeout(0);if(document.getElementById('modalCustom')!=undefined){$('#modalCustom').stop();document.body.removeChild(document.getElementById('modalCustom'));}var modelDiv = document.createElement('DIV');modelDiv.setAttribute('style', 'position: fixed;top: 80%;left: 25%;display: inline-block;height: auto;z-index: 2000;');modelDiv.setAttribute('id', 'modalCustom');var modelDivSpan = document.createElement('SPAN');modelDivSpan.setAttribute('style', 'color: #FFF;background: rgba(0, 0, 0, 0.5);position: relative;border-radius: 2px;margin: 0px auto;padding: 5px 10px;max-width: 300px;text-overflow: ellipsis;overflow: hidden;white-space: nowrap;');var txt = document.createTextNode('" + message + "');modelDivSpan.appendChild(txt);modelDiv.appendChild(modelDivSpan);document.body.appendChild(modelDiv);$('#modalCustom').fadeOut(4000, function() {document.body.removeChild(document.getElementById('modalCustom'));});} createModel();</script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), null, showtoast);
        }

        public static void showAlert(Page page,string message)
        {
            string showalert = "<script type='text/javascript'>alert('"+message+"');</script>";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), null,showalert);
        }

        public static void printPage(Page page)
        {
            string printpage = "<script type='text/javascript'>window.print();</script>";
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), null, printpage);
        }


        public static void showLoad(Page page)
        {
            string showload = @"<script type='text/javascript'>
                                    function Loadshow(){ 
                                        var load = document.createElement('div');
                                        load.setAttribute('id','load');
                                        load.setAttribute('style','position: fixed;width:100px;height: 80px;margin: 230px 48%;text-align: center;border-radius: 5px;z-index: 999;background-color: rgba(0,0,0,.3);');
                                        var span = document.createElement('span');
                                        span.setAttribute('style','line-height: 80px;');
                                        var spanText = document.createTextNode('载入中...');
                                        span.appendChild(spanText);
                                        load.appendChild(span);
                                        document.body.appendChild(load);
                                    };
                                    Loadshow();
                                </script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "show", showload);
            PageUtil.hiddenLoad(page);
        }

        public static void hiddenLoad(Page page)
        {
            string hiddenload = @"<script type='text/javascript'>
                                        function hiddenDiv(){
				                            var timer=setTimeout(function Loadhidden(){
				                                var load = document.getElementById('load');
				                                document.body.removeChild(load);
			                                },800);
			                            };
                                        hiddenDiv();
                                   </script>";
            page.ClientScript.RegisterStartupScript(page.GetType(), "hide", hiddenload);
        }
    }
}