using AutoMapper;
using ServerQlsvAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ServerQlsvAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SinhVienApiController : ApiController
    {
        QLSVEntities ctx = new QLSVEntities();

        #region Helper
        public HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T data)
        {
            return Request.CreateResponse(statusCode, data);
        }

        public HttpResponseMessage CreateResponse(HttpStatusCode statusCode)
        {
            return Request.CreateResponse(statusCode);
        }
        #endregion

        [HttpGet]
        [Route("api/sinhvien/all")]
        public IHttpActionResult GetAll()
        {
            List<SINHVIEN> list = ctx.SINHVIENs.ToList();

            Mapper.CreateMap<SINHVIEN, SinhVienModel>();
            var result = Mapper.Map<List<SINHVIEN>, List<SinhVienModel>>(list);

            return Ok(result);
        }

        /*[HttpGet]
        [Route("api/sinhvien/get")]
        public HttpResponseMessage GetSinhVien([FromUri]string mssv)
        {
            using (QLSVEntities ctx = new QLSVEntities())
            {
                var sv = ctx.SINHVIENs.Where(t => t.MaSoSinhVien == mssv).FirstOrDefault();

                if (sv == null)
                {
                    return CreateResponse(HttpStatusCode.BadRequest);
                }

                return CreateResponse(HttpStatusCode.OK, sv);
            }
        }*/

        [HttpGet]
        [Route("api/sinhvien/get")]
        public IHttpActionResult GetSinhVien([FromUri]string mssv)
        {
            int ms = int.Parse(mssv);
            var sv = ctx.SINHVIENs.Where(t => t.MaSoSinhVien == ms).FirstOrDefault();

            if (sv == null)
            {
                return BadRequest();
            }

            return Ok(sv);
        }

        [HttpPost]
        [Route("api/sinhvien/add")]
        public IHttpActionResult AddSinhVien([FromBody]SINHVIEN newInfo)
        {
            //SINHVIEN sv = newInfo;
            ctx.SINHVIENs.Add(newInfo);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("api/sinhvien/update")]
        public HttpResponseMessage UpdateSinhVien([FromUri]string mssv, [FromBody]SINHVIEN newInfo)
        {
            int ms = int.Parse(mssv);
            var sv = ctx.SINHVIENs.Where(t => t.MaSoSinhVien == ms).FirstOrDefault();

            if (sv == null)
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }

            sv.HoTen = newInfo.HoTen;
            sv.NgaySinh = newInfo.NgaySinh;
            sv.SoDienThoai = newInfo.SoDienThoai;
            sv.DiemTichLuy = newInfo.DiemTichLuy;

            int result = ctx.SaveChanges();

            return CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("api/sinhvien/delete")]
        public HttpResponseMessage DeleteSinhVien([FromUri]string mssv)
        {
            int ms = int.Parse(mssv);
            var sv = ctx.SINHVIENs.Where(t => t.MaSoSinhVien == ms).FirstOrDefault();

            if (sv == null)
            {
                return CreateResponse(HttpStatusCode.BadRequest);
            }

            ctx.SINHVIENs.Remove(sv);
            int result = ctx.SaveChanges();

            return CreateResponse(HttpStatusCode.OK, result);

        }
    }
}
