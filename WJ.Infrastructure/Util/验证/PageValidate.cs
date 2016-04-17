using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace WJ.Infrastructure.Util
{
	/// <summary>
	/// ҳ������У����
	/// ����ƽ
	/// 2004.8
	/// </summary>
	public class PageValidate
	{
        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
		private static Regex RegNumber = new Regex("^[0-9]+$");
		private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
		private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
		private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
		private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

		public PageValidate()
		{
		}
		#region �����ַ������		
        public static bool IsPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
		/// <summary>
		/// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
		/// </summary>
		/// <param name="req">Request</param>
		/// <param name="inputKey">Request�ļ�ֵ</param>
		/// <param name="maxLen">��󳤶�</param>
		/// <returns>����Request��ѯ�ַ���</returns>
		public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
		{
			string retVal = string.Empty;
			if(inputKey != null && inputKey != string.Empty)
			{
				retVal = req.QueryString[inputKey];
				if(null == retVal)
					retVal = req.Form[inputKey];
				if(null != retVal)
				{
					retVal = SqlText(retVal, maxLen);
					if(!IsNumber(retVal))
						retVal = string.Empty;
				}
			}
			if(retVal == null)
				retVal = string.Empty;
			return retVal;
		}		
		/// <summary>
		/// �Ƿ������ַ���
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsNumber(string inputData)
		{
			Match m = RegNumber.Match(inputData);
			return m.Success;
		}

		/// <summary>
		/// �Ƿ������ַ��� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsNumberSign(string inputData)
		{
			Match m = RegNumberSign.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// �Ƿ��Ǹ�����
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsDecimal(string inputData)
		{
			Match m = RegDecimal.Match(inputData);
			return m.Success;
		}		
		/// <summary>
		/// �Ƿ��Ǹ����� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsDecimalSign(string inputData)
		{
			Match m = RegDecimalSign.Match(inputData);
			return m.Success;
		}		

		#endregion

		#region ���ļ��

		/// <summary>
		/// ����Ƿ��������ַ�
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static bool IsHasCHZN(string inputData)
		{
			Match m = RegCHZN.Match(inputData);
			return m.Success;
		}	

		#endregion

		#region �ʼ���ַ
		/// <summary>
		/// �Ƿ��Ǹ����� �ɴ�������
		/// </summary>
		/// <param name="inputData">�����ַ���</param>
		/// <returns></returns>
		public static bool IsEmail(string inputData)
		{
			Match m = RegEmail.Match(inputData);
			return m.Success;
		}		

		#endregion

        #region ���ڸ�ʽ�ж�
        /// <summary>
        /// ���ڸ�ʽ�ַ����ж�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region ����

        /// <summary>
		/// ����ַ�����󳤶ȣ�����ָ�����ȵĴ�
		/// </summary>
		/// <param name="sqlInput">�����ַ���</param>
		/// <param name="maxLength">��󳤶�</param>
		/// <returns></returns>			
		public static string SqlText(string sqlInput, int maxLength)
		{			
			if(sqlInput != null && sqlInput != string.Empty)
			{
				sqlInput = sqlInput.Trim();							
				if(sqlInput.Length > maxLength)//����󳤶Ƚ�ȡ�ַ���
					sqlInput = sqlInput.Substring(0, maxLength);
			}
			return sqlInput;
		}		
		/// <summary>
		/// �ַ�������
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string HtmlEncode(string inputData)
		{
			return HttpUtility.HtmlEncode(inputData);
		}
		/// <summary>
		/// ����Label��ʾEncode���ַ���
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="txtInput"></param>
		public static void SetLabel(Label lbl, string txtInput)
		{
			lbl.Text = HtmlEncode(txtInput);
		}
		public static void SetLabel(Label lbl, object inputObj)
		{
			SetLabel(lbl, inputObj.ToString());
		}		
		//�ַ�������
		public static string InputText(string inputString, int maxLength) 
		{			
			StringBuilder retVal = new StringBuilder();

			// ����Ƿ�Ϊ��
			if ((inputString != null) && (inputString != String.Empty)) 
			{
				inputString = inputString.Trim();
				
				//��鳤��
				if (inputString.Length > maxLength)
					inputString = inputString.Substring(0, maxLength);
				
				//�滻Σ���ַ�
				for (int i = 0; i < inputString.Length; i++) 
				{
					switch (inputString[i]) 
					{
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}				
				retVal.Replace("'", " ");// �滻������
			}
			return retVal.ToString();
			
		}
		/// <summary>
		/// ת���� HTML code
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Encode(string str)
		{			
			str = str.Replace("&","&amp;");
			str = str.Replace("'","''");
			str = str.Replace("\"","&quot;");
			str = str.Replace(" ","&nbsp;");
			str = str.Replace("<","&lt;");
			str = str.Replace(">","&gt;");
			str = str.Replace("\n","<br>");
			return str;
		}
		/// <summary>
		///����html�� ��ͨ�ı�
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Decode(string str)
		{			
			str = str.Replace("<br>","\n");
			str = str.Replace("&gt;",">");
			str = str.Replace("&lt;","<");
			str = str.Replace("&nbsp;"," ");
			str = str.Replace("&quot;","\"");
			return str;
		}

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//ȥ��,
            sqlText = sqlText.Replace("<", "");//ȥ��<
            sqlText = sqlText.Replace(">", "");//ȥ��>
            sqlText = sqlText.Replace("--", "");//ȥ��--
            sqlText = sqlText.Replace("'", "");//ȥ��'
            sqlText = sqlText.Replace("\"", "");//ȥ��"
            sqlText = sqlText.Replace("=", "");//ȥ��=
            sqlText = sqlText.Replace("%", "");//ȥ��%
            sqlText = sqlText.Replace(" ", "");//ȥ���ո�
            return sqlText;
        }
		#endregion

        #region �Ƿ����ض��ַ����
        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                //Regex RegNumber = new Regex(string.Format("^([{0}]{{1}})+$", charInput,lenInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region �������Ĳ����ǲ���ĳЩ����õ������ַ����������Ŀǰ������������İ�ȫ���
        /// <summary>
        /// �������Ĳ����ǲ���ĳЩ����õ������ַ����������Ŀǰ������������İ�ȫ���
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion


        /// <summary>
        /// �Ƿ�Ϊ����ĸ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetter(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }
        /// <summary>
        /// �Ƿ�ΪͼƬ��ʽ
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsImage(string filename)
        {
            return Regex.IsMatch(filename, @"(?i)\.(jpg|gif|png|bmp|tiff)$");
        }
        /// <summary>
        /// ��֤�Ƿ�Ϊ�û�����ʽ����ĸ�����֡��»��ߣ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUsername(string str)
        {
            return Regex.IsMatch(str, @"^\w+$");
        }

        public static bool IsUrl(string str)
        {
            return Regex.IsMatch(str, @"(?i)^http(?s)://([\w\-]+\.)+[\w\-]+(/[\w\-./?%&=]*)?$");
        }


        public static bool IsIpAddress(string str)
        {
            return Regex.IsMatch(str, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
        }
        /// <summary>
        /// ��֤�Ƿ�Ϊ���֤��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIDCardNumber(string str)
        {
            if (str.Length == 15)
            {
                return Regex.IsMatch(str, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$");
            }
            else if (str.Length == 18)
            {
                return Regex.IsMatch(str, @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}(\d|x|X)$");
            }
            return false;
        }
        /// <summary>
        /// �Ƿ�Ϊ�ֻ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(string str)
        {
            return Regex.IsMatch(str, @"^(13[0-9]|15[0|3|6|7|8|9]|18[8|9])\d{8}$");
        }
        /// <summary>
        /// �Ƿ�Ϊ�绰��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTelphone(string str)
        {
            string patt = @"^((\d{7,8})|(\d{3,4})(-|\s)?(\d{7,8})|(\d{3,4})(-|\s)?(\d{7,8})(-|\s)(\d{1,4})|(\d{7,8})(-|\s)(\d{1,4}))$";
            return Regex.IsMatch(str, patt);
        }
        /// <summary>
        /// �Ƿ�Ϊ���ڸ�ʽ������ʱ�䣩
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(string str)
        {
            string patt = @"^(?:(?!0000)[0-9]{4}([-/.\s]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.\s]?)0?2\2(?:29))$";
            return Regex.IsMatch(str, patt);
        }
        /// <summary>
        /// �Ƿ�Ϊʱ���ʽ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTime(string str)
        {
            return Regex.IsMatch(str, @"^([01]?[0-9]|2?[0-3]):[0-5]?[0-9](:[0-5]?[0-9])?$");
        }

    }
}
