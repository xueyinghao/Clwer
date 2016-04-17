using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace WJ.Infrastructure.Util
{
    public class ResponHtml : System.Web.UI.Page
    {
        /// <summary>
        /// 绑定视频播放
        /// </summary>
        /// <param name="width">播放器宽度</param>
        /// <param name="height">播放器高度</param>
        /// <param name="link">播放文件地址</param>
        /// <returns></returns>
        public static string MediaPlayer(int width, int height, string link,string Balance)
        {
            string str = "";
            try
            {
                str += "<object id='player' height='" + height + "' width='" + width + "' classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' type=application/x-oleobject>";
                str += "<param NAME='AutoStart' VALUE='-1'>";
                //str += "<!--是否自动播放-->";
                str += "<param NAME='Balance' VALUE='"+Balance+"'>";
                //str += "<!--调整左右声道平衡,同上面旧播放器代码-->";
                str += "<param name='enabled' value='1'>";
                //str += "<!--播放器是否可人为控制-->";
                str += "<param NAME='EnableContextMenu' VALUE='-1'>";
                //str += "<!--是否启用上下文菜单-->";
                str += "<param NAME='url' value='" + link + "'>";
                //str += "<!--播放的文件地址-->";
                str += "<param NAME='PlayCount' VALUE='10'>";
                //str += "<!--播放次数控制,为整数-->";
                str += "<param name='rate' value='1'>";
                //str += "<!--播放速率控制,1为正常,允许小数,1.0-2.0-->";
                str += "<param name='currentPosition' value='0'>";
                //str += "<!--控件设置:当前位置-->";
                str += "<param name='currentMarker' value='0'>";
                //str += "<!--控件设置:当前标记-->";
                str += "<param name='defaultFrame' value=''>";
                //str += "<!--显示默认框架-->";
                str += "<param name='invokeURLs' value='0'>";
                //str += "<!--脚本命令设置:是否调用URL-->";
                str += "<param name='baseURL' value=''>";
                //str += "<!--脚本命令设置:被调用的URL-->";
                str += "<param name='stretchToFit' value='1'>";
                //str += "<!--是否按比例伸展-->";
                str += "<param name='volume' value='100'>";
                //str += "<!--默认声音大小0%-100%,50则为50%-->";
                str += "<param name='mute' value='0'>";
                //str += "<!--是否静音-->";
                str += "<param name='uiMode' value='Full'>";
                //str += "<!--播放器显示模式:Full显示全部;mini最简化;None不显示播放控制,只显示视频窗口;invisible全部不显示-->";
                str += "<param name='windowlessVideo' value='0'>";
                //str += "<!--如果是0可以允许全屏,否则只能在窗口中查看-->";
                str += "<param name='fullScreen' value='0'>";
                //str += "<!--开始播放是否自动全屏-->";
                str += "<param name='enableErrorDialogs' value='-1'>";
                //str += "<!--是否启用错误提示报告-->";
                str += "<param name='SAMIStyle' value>";
                //str += "<!--SAMI样式-->";
                str += "<param name='SAMILang' value>";
                //str += "<!--SAMI语言-->";
                str += "<param name='SAMIFilename' value>";
                //str += "<!--字幕ID-->";
                str += "</object>";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return str;
        }


        /// <summary>
        /// 绑定视频播放
        /// </summary>
        /// <param name="width">播放器宽度</param>
        /// <param name="height">播放器高度</param>
        /// <param name="link">播放文件地址</param>
        /// <returns></returns>
        public static string MediaPlayer_Flash(int width, int height, string link,string flashplayerpath)
        {
            string str = "";
            try
            {
                str += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\"";
                str += "width=\"" + width + "\" height=\"" + height + "\">";
                str += "<param name=\"movie\" value=\"" + flashplayerpath + "\"/>";
                str += "<param name=\"quality\" value=\"high\" />";
                str += " <param name=\"allowFullScreen\" value=\"true\" />";
                str += " <param name=\"FlashVars\" value=\"vcastr_file=" + link + "&BufferTime=3\" />";
                str += " <embed src=\"flash/flvplayer.swf\"";
                str += "allowfullscreen=\"true\" flashvars=\"vcastr_file=" + link + "\"";
                str += " quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"";
                str += "width=\""+width+"\" height=\""+height+"\"></embed>";
                str += " </object>";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return str;
        }

        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="width">最大宽度</param>
        /// <param name="url">路径</param>
        /// <returns></returns>
        public static string responImg(int width, string url)
        {
            return "<img onload=\"javascript:if(this.width>" + width + ")this.width=" + width + "\"  src=\"" + url + "\" />";
        }



        /// <summary>
        /// 输出swf
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string responswf(int width, int height, string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"" + width + "\" height=\"" + height + "\">");
            sb.Append("<param name=\"movie\" value=\"" + path + "\" />");
            sb.Append("<param name=\"quality\" value=\"high\" />");
            sb.Append("<param name=\"wmode\" value=\"opaque\" />");
            sb.Append("<embed src=\"" + path + "\" quality=\"high\"  wmode=\"opaque\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"" + width + "\" height=\"" + height + "\"></embed></object>");
            return sb.ToString();
        }
    }
}
