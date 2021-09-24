using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyUSC.Classes
{
	public class AdRequest : USCBaseItem
    {
#region members
        private string m_strCompanyName;
        private string m_strAddress;
        private string m_strCity;
        private string m_strState;
        private string m_strZip;
        private string m_strFirstName;
        private string m_strLastName;
        private string m_strWorkPhone;
        private string m_strCellPhone;
        private string m_strEmail;
        private string m_strWebsite;
        private bool m_bLocalAds;
        private bool m_bNationalAds;
        private string m_strComments;
#endregion
        private void Init()
        {
            m_lKey = 0;
            m_strCompanyName = "";
            m_strAddress = "";
            m_strCity = "";
            m_strState = "";
            m_strZip = "";
            m_strFirstName = "";
            m_strLastName = "";
            m_strWorkPhone = "";
            m_strCellPhone = "";
            m_strEmail = "";
            m_strWebsite = "";
            m_bLocalAds = false;
            m_bNationalAds = false;
            m_strComments = "";
            m_strCreator = "";
            m_dtCreateDate = DateTime.Now;
            m_strLastUpdate = "";
        }

        public AdRequest()
        {
            Init();
        }

#region Accessors
        public string CompanyName
        {
            get
            {
                return this.m_strCompanyName;
            }
            set
            {
                this.m_strCompanyName = value;
            }
        }

        public string Address
        {
            get
            {
                return this.m_strAddress;
            }
            set
            {
                this.m_strAddress = value;
            }
        }

        public string City
        {
            get
            {
                return this.m_strCity;
            }
            set
            {
                this.m_strCity = value;
            }
        }

        public string State
        {
            get
            {
                return this.m_strState;
            }
            set
            {
                this.m_strState = value;
            }
        }

        public string Zip
        {
            get
            {
                return this.m_strZip;
            }
            set
            {
                this.m_strZip = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.m_strFirstName;
            }
            set
            {
                this.m_strFirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.m_strLastName;
            }
            set
            {
                this.m_strLastName = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return this.m_strWorkPhone;
            }
            set
            {
                this.m_strWorkPhone = value;
            }
        }

        public string CellPhone
        {
            get
            {
                return this.m_strCellPhone;
            }
            set
            {
                this.m_strCellPhone = value;
            }
        }

        public string Email
        {
            get
            {
                return this.m_strEmail;
            }
            set
            {
                this.m_strEmail = value;
            }
        }

        public string Website
        {
            get
            {
                return this.m_strWebsite;
            }
            set
            {
                this.m_strWebsite = value;
            }
        }

        public bool LocalAds
        {
            get
            {
                return this.m_bLocalAds;
            }
            set
            {
                this.m_bLocalAds = value;
            }
        }

        public bool NationalAds
        {
            get
            {
                return this.m_bNationalAds;
            }
            set
            {
                this.m_bNationalAds = value;
            }
        }

        public string Comments
        {
            get
            {
                return this.m_strComments;
            }
            set
            {
                this.m_strComments = value;
            }
        }

#endregion
    }

	public sealed class AdRequestList : USCBaseList
    {
        #region Column Constants
        const int colKey = 0;
        const int colCompanyName = 1;
        const int colAddress = 2;
        const int colCity = 3;
        const int colState = 4;
        const int colZip = 5;
        const int colFirstName = 6;
        const int colLastName = 7;
		const int colWorkPhone = 8;
		const int colCellPhone = 9;
		const int colEmail = 10;
		const int colWebsite = 11;
		const int colLocalAds = 12;
		const int colNationalAds = 13;
		const int colComments = 14;
		const int colCreator = 15;
		const int colCreateDate = 16;
		const int colLastUpdate = 17;
		#endregion

        private static volatile AdRequestList instance = null;
        private static object syncRoot = new object();
		private string m_strCnxString;

		const string strSQLGetAllAdRequests = "SELECT * FROM Advertise";
		const string strSQLNewAdRequests = "INSERT into Advertise (CompanyName,Address,City,State,Zipcode,ContactFirstName,ContactLastName,WorkPhone,CellPhone,Email,CompanyWebsite,LocalAdvertising,NationalAdvertising,Comments,CreationUser,CreationDate,LastUpdateUserTime) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},{12},'{13}','{14}','{15}','{16}');";

        private AdRequestList()
        {
        }

        public static AdRequestList Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AdRequestList();
                    }
                }

                return instance;
            }
        }

        public Hashtable htAdRequest;


		public void Init(string cnxString, bool fForce)
		{
			m_strCnxString = cnxString;
			if (null == htAdRequest || fForce)
			{
				htAdRequest = new Hashtable();
				Load();
			}
		}

        public bool Load()
        {
            bool fRet = false;

            htAdRequest.Clear();

            SqlConnection sqlConn = null;
            DataSet locStrDS = new DataSet();

            try
            {
				sqlConn = new SqlConnection(m_strCnxString);
                sqlConn.Open();
                SqlDataAdapter daLocStrings = new SqlDataAdapter(strSQLGetAllAdRequests, sqlConn);
				daLocStrings.Fill(locStrDS, "Advertise");
            }
            catch (Exception ex)
            {
				EvtLog.WriteException("AdRequest.Load failure", ex, 0);
                return false;
            }
            finally
            {
                sqlConn.Close();
            }

			DataRowCollection dra = locStrDS.Tables["Advertise"].Rows;
            foreach (DataRow dr in dra)
            {
                AdRequest item = new AdRequest();
				fRet = ReadAdRequest(dr, item);
                if (fRet)
                {
					htAdRequest.Add(item.Key, item);
                }
            }
            fRet = true;

            return fRet;
        }

		private bool ReadAdRequest(DataRow dr, AdRequest item)
        {
            bool fRet = true;
            try
            {
				item.Key = (long)dr.ItemArray[colKey];
				item.CompanyName = dr.ItemArray[colCompanyName].ToString();
				item.Address = dr.ItemArray[colAddress].ToString();
				item.City = dr.ItemArray[colCity].ToString();
				item.State = dr.ItemArray[colState].ToString();
				item.Zip = dr.ItemArray[colZip].ToString();
				item.FirstName = dr.ItemArray[colFirstName].ToString();
				item.LastName = dr.ItemArray[colLastName].ToString();
				item.WorkPhone = dr.ItemArray[colWorkPhone].ToString();
				item.CellPhone = dr.ItemArray[colCellPhone].ToString();
				item.Email = dr.ItemArray[colEmail].ToString();
				item.Website = dr.ItemArray[colWebsite].ToString();
				item.LocalAds = bool.Parse(dr.ItemArray[colLocalAds].ToString());
				item.NationalAds = bool.Parse(dr.ItemArray[colNationalAds].ToString());
				item.Comments = dr.ItemArray[colComments].ToString();
				item.Creator = dr.ItemArray[colCreator].ToString();
				//item.CreateDate = DateTime.Parse(dr.ItemArray[colCreateDate].ToString());
				item.LastUpdate = dr.ItemArray[colLastUpdate].ToString();
            }
            catch (Exception ex)
            {
				EvtLog.WriteException("AdRequest.ReadAdRequest failure", ex, 0);
				fRet = false;
            }
            return fRet;
        }

        public int Count
        {
            get
            {
                return htAdRequest.Count;
            }
        }

        public AdRequest GetAdRequest(long index)
        {
            return (AdRequest)htAdRequest[index];
        }

        public bool Add(AdRequest item)
        {
            bool fRet = false;
			SqlConnection sqlConn = null;
			int nLocalAds = 0;
			int nNationalAds = 0;
			if( item.LocalAds )
			{
				nLocalAds = 1;
			}
			if (item.NationalAds)
			{
				nNationalAds = 1;
			}
			string strSQLQuery = String.Format(strSQLNewAdRequests,
												Sanitize(item.CompanyName),
												Sanitize(item.Address),
												Sanitize(item.City),
												Sanitize(item.State),
												Sanitize(item.Zip),
												Sanitize(item.FirstName),
												Sanitize(item.LastName),
												Sanitize(item.WorkPhone),
												Sanitize(item.CellPhone),
												Sanitize(item.Email),
												Sanitize(item.Website),
												nLocalAds,
												nNationalAds,
												Sanitize(item.Comments),
												Sanitize(item.Creator),
												item.CreateDate,
												item.LastUpdate
												);

			try
			{
				sqlConn = new SqlConnection(m_strCnxString);
				sqlConn.Open();
				SqlCommand sqlCommand = new SqlCommand(strSQLQuery,sqlConn);
				sqlCommand.ExecuteNonQuery();

				// Create another Command to get IDENTITY Value
				sqlCommand.Parameters.Clear();
				sqlCommand.CommandText = "SELECT @@IDENTITY";
				// Get the last inserted id.
				int insertID = Convert.ToInt32(sqlCommand.ExecuteScalar());
				item.Key = insertID;
				sqlConn.Close();

				if (0 < insertID)
				{
					htAdRequest.Add(item.Key, item);
				}
				fRet = true;
			}
			catch (Exception ex)
			{
				EvtLog.WriteException("AdRequest.Add failure", ex, 0);
				return false;
			}
			finally
			{
				sqlConn.Close();
			}
			return fRet;
        }

		public bool Update(AdRequest item)
        {
            bool fRet = false;
            return fRet;
        }

		public bool Delete(AdRequest item)
        {
            bool fRet = false;
            return fRet;
        }

    }
}