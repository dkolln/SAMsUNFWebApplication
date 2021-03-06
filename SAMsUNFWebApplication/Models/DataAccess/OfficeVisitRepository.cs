﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SAMsUNFWebApplication.Models;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace SAMsUNFWebApplication.Models.DataAccess
{
    public class OfficeVisitRepository
    {
        private MySqlConnection _openConnection;


        public OfficeVisitRepository(MySqlConnection openConnection)
        {
            this._openConnection = openConnection;
        }


        public async Task<IEnumerable<OfficeVisit>> GetALLOfficeVisits()
        {
            // Read the user by their username in the database. 
            IEnumerable<OfficeVisit> results = await this._openConnection.QueryAsync<OfficeVisit>(@" SELECT * FROM  vw_office_visit");

            return results;
        }

        public async Task<OfficeVisit> GetOfficeVisitByID(int officeVisitID)
        {
            // Read the user by their username in the database. 
            var results = await this._openConnection.QueryAsync<OfficeVisit>(@" SELECT * FROM  vw_office_visit WHERE office_visit_id = @officeVisitID",
                    new { officeVisitID = officeVisitID });
            return results.FirstOrDefault(); ;
        }

        public bool InsertOfficeVisit(OfficeVisit officeVisit)
        {
            bool success = false;

            var queryString = @"INSERT INTO office_visit values(@student_id ,@total_visits,@content_course_id,@sent_by_contact_id" +
                              ",@office_visit_dt,@arrival_dt,@handled_by_contact_id`,@nap,@comment,@last_update_contact_id,now())";

            try
            {
                this._openConnection.Execute(queryString, new
                {
                    officeVisit.student_number,
                    officeVisit.total_visits,
                    officeVisit.content_course_id,
                    officeVisit.sent_by_contact_id,
                    officeVisit.office_visit_dt,
                    officeVisit.arrival_dt,
                    officeVisit.handled_by_contact_id,
                    officeVisit.nap,
                    officeVisit.comments,
                    officeVisit.last_update_contact_id
                });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return success;
        }

        public bool UpdateOfficeVisit(OfficeVisit officeVisit)
        {
            bool success = false;

            var queryString = @"UPDATE  office_visit values(@student_id ,@total_visits,@content_course_id,@sent_by_contact_id" +
                              ",@office_visit_dt,@arrival_dt,@handled_by_contact_id`,@nap,@comment,@last_update_contact_id,now())";

            try
            {
                this._openConnection.Execute(queryString, new
                {
                    officeVisit.student_id,
                    officeVisit.total_visits,
                    officeVisit.content_course_id,
                    officeVisit.sent_by_contact_id,
                    officeVisit.office_visit_dt,
                    officeVisit.arrival_dt,
                    officeVisit.handled_by_contact_id,
                    officeVisit.nap,
                    officeVisit.comments,
                    officeVisit.last_update_contact_id
                });
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return success;
        }

        public async Task<IEnumerable<OfficeVisitsByHomeroom>> GetOfficeVisitsByHomeroom()
        {
            IEnumerable<OfficeVisitsByHomeroom> results = await this._openConnection.QueryAsync<OfficeVisitsByHomeroom>(@" SELECT * FROM  vw_office_visits_by_homeroom");
            return results;
        }

        public async Task<IEnumerable<OfficeVisitsByTeacher>> GetOfficeVisitsByTeacher()
        {
            IEnumerable<OfficeVisitsByTeacher> results = await this._openConnection.QueryAsync<OfficeVisitsByTeacher>(@" SELECT * FROM  vw_office_visits_by_teacher");
            return results;
        }

        public async Task<IEnumerable<OfficeVisitsByOffenseType>> GetOfficeVisitsByOffenseType()
        {
            IEnumerable<OfficeVisitsByOffenseType> results = await this._openConnection.QueryAsync<OfficeVisitsByOffenseType>(@" SELECT * FROM  vw_office_visits_by_offense_type");
            return results;

        }

        public async Task<IEnumerable<OfficeVisitsCountsByWeek>> GetOfficeVisitsCountByWeek()
        {
            IEnumerable<OfficeVisitsCountsByWeek> results = await this._openConnection.QueryAsync<OfficeVisitsCountsByWeek>(@" SELECT * FROM  vw_office_visits_by_weekly_count");
            return results;

        }
    }
}