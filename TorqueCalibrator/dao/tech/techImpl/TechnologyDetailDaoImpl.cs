
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using TorqueCalibrator.pojo;
using TorqueCalibrator.untils;

namespace TorqueCalibrator.dao.tech.techImpl
{
    public class TechnologyDetailDaoImpl : TechnologyDetailDao
    {
        public List<TechnologyDetail> SelectPreCheckList(string toolId)
        {
            string query_string = "SELECT ID id ,TOOL_ID ToolID, PRECHECK_PRE_COUNT PreNum,PRECHECK_STANDARD Standard,PRECHECK_UPPER PreUpper,PRECHECK_LOWER PreLower,PRECHECK_BACK_COUNT PostNum,PRECHECK_BACK_UPPER PostUpper,PRECHECK_BACK_LOWER PostLower from `base_tool_precheck` where ID=@toolId and DEL_FLAG=0";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@toolId", toolId)});
            List<TechnologyDetail> ls = MESMysqlTool.toList<TechnologyDetail>(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //TechnologyDetail temp = new TechnologyDetail();
                ls[i].Id = dt.Rows[i]["id"].ToString();
                ls[i].Num = int.Parse(dt.Rows[i]["PreNum"].ToString());
                ls[i].Standard = float.Parse(dt.Rows[i]["Standard"].ToString());
                ls[i].Upper = float.Parse(dt.Rows[i]["PreUpper"].ToString());
                ls[i].Lower = float.Parse(dt.Rows[i]["PreLower"].ToString());
                //ls.Add(temp);
            }
            return ls.Count > 0 ? ls : null;
        }

        public List<TechnologyDetail> SelectPostCheckList(string toolId)
        {
            string query_string = "SELECT ID id ,TOOL_ID ToolID, PRECHECK_PRE_COUNT PreNum,PRECHECK_STANDARD Standard,PRECHECK_UPPER PreUpper,PRECHECK_LOWER PreLower,PRECHECK_BACK_COUNT PostNum,PRECHECK_BACK_UPPER PostUpper,PRECHECK_BACK_LOWER PostLower from `base_tool_precheck` where ID=@toolId and DEL_FLAG=0";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@toolId", toolId)});
            List<TechnologyDetail> ls = MESMysqlTool.toList<TechnologyDetail>(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //TechnologyDetail temp = new TechnologyDetail();
                ls[i].Id = dt.Rows[i]["id"].ToString();
                ls[i].Num = int.Parse(dt.Rows[i]["PostNum"].ToString());
                ls[i].Standard = float.Parse(dt.Rows[i]["Standard"].ToString());
                ls[i].Upper = float.Parse(dt.Rows[i]["PostUpper"].ToString());
                ls[i].Lower = float.Parse(dt.Rows[i]["PostLower"].ToString());
                //ls.Add(temp);
            }
            return ls.Count > 0 ? ls : null;
        }

        public List<TechnologyDetail> selectList(string toolId)
        {
            string query_string = "SELECT ID id ,TOOL_ID ToolID, PRECHECK_PRE_COUNT PreNum,PRECHECK_STANDARD Standard,PRECHECK_UPPER PreUpper,PRECHECK_LOWER PreLower,PRECHECK_BACK_COUNT PostNum,PRECHECK_BACK_UPPER PostUpper,PRECHECK_BACK_LOWER PostLower from `base_tool_precheck` where TOOL_ID=@toolId and DEL_FLAG=0";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@toolId", toolId)});
            List<TechnologyDetail> ls = MESMysqlTool.toList<TechnologyDetail>(dt);
            return ls.Count > 0 ? ls : null;
        }


        public List<TechnologyDetail> selectPreList(string toolId)
        {
            string query_string = "SELECT ID Id,PRECHECK_PRE_COUNT PreNum,PRECHECK_STANDARD Standard,PRECHECK_UPPER PreUpper,PRECHECK_LOWER PreLower,PRECHECK_BACK_COUNT PostNum,PRECHECK_BACK_UPPER PostUpper,PRECHECK_BACK_LOWER PostLower from `base_tool_precheck` where TOOL_ID=@toolId and DEL_FLAG=0";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@toolId", toolId)});
            List<TechnologyDetail> ls = new List<TechnologyDetail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TechnologyDetail temp = new TechnologyDetail();
                temp.Id = dt.Rows[i]["id"].ToString();
                temp.Num = int.Parse(dt.Rows[i]["PreNum"].ToString());
                temp.Standard = float.Parse(dt.Rows[i]["Standard"].ToString());
                temp.Upper = float.Parse(dt.Rows[i]["PreUpper"].ToString());
                temp.Lower = float.Parse(dt.Rows[i]["PreLower"].ToString());
                ls.Add(temp);
            }
            return ls.Count > 0 ? ls : null;
        }

        public List<TechnologyDetail> selectPostList(string toolId)
        {
            string query_string = "SELECT ID Id,PRECHECK_PRE_COUNT PreNum,PRECHECK_STANDARD Standard,PRECHECK_UPPER PreUpper,PRECHECK_LOWER PreLower,PRECHECK_BACK_COUNT PostNum,PRECHECK_BACK_UPPER PostUpper,PRECHECK_BACK_LOWER PostLower from `base_tool_precheck` where TOOL_ID=@toolId and DEL_FLAG=0";
            DataTable dt = MESMysqlTool.queryData(query_string, new MySqlParameter[] { 
                    new MySqlParameter("@toolId", toolId)});
            List<TechnologyDetail> ls = new List<TechnologyDetail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TechnologyDetail temp = new TechnologyDetail();
                temp.Id = dt.Rows[i]["id"].ToString();
                temp.Num = int.Parse(dt.Rows[i]["PostNum"].ToString());
                temp.Standard = float.Parse(dt.Rows[i]["Standard"].ToString());
                temp.Upper = float.Parse(dt.Rows[i]["PostUpper"].ToString());
                temp.Lower = float.Parse(dt.Rows[i]["PostLower"].ToString());
                ls.Add(temp);
            }
            return ls.Count > 0 ? ls : null;
        }
    }
}
