using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
   
    public class StudentController : ApiController
    {

        private IMapper mapper;
        private readonly StudentService studentService = new StudentService(new StudentRepository(UniversityContext.Create()));

        public StudentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }



        /// <summary>
        /// obtiene los objetos de estudiante
        /// </summary>
        /// <returns>  listado de los objetos de estudiante  </returns>
        ///  <response code="200">OK. Devuleve el listado de objetos solicitados </response>


        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var students = await studentService.GetAll();
            var studentsDTO = students.Select(x => mapper.Map<StudentDTO>(x));

            return Ok(studentsDTO);
        }


        /// <summary>
        ///  Obtiene un objeto estudiante por su ID
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su ID.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto estudiante </returns>
        /// <response code="200">OK. Devuleve el objetos solicitados </response>
        /// <response code="404">Not Found. No se ah encontrado el objeto solicitado </response>

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var student = await studentService.GetById(id);

            if (student == null)
                return NotFound();


            var studentDTO = mapper.Map<StudentDTO>(student);

            return Ok(studentDTO);
        }

        /// <summary>
        /// Inserta un objeto a estudiante
        /// </summary>
        /// <remarks>
        /// Todos los campos son necesarios llenar
        /// </remarks>
        /// <returns>Objeto Insertado en estudiante</returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>

        [HttpPost]
        public async Task<IHttpActionResult> Post(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var student = mapper.Map<Student>(studentDTO);
                student = await studentService.Insert(student);
                return Ok(student);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        /// <summary>
        /// Actualiza un objeto a estudiante
        /// </summary>
        /// <remarks>
        /// Actualizar los campos deseados
        /// </remarks>
        /// <returns>Objeto Actualizado en estudiante </returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>
        /// <response code="404">Not Found. No se ah Actualizado el objeto solicitado</response>

        [HttpPut]
        public async Task<IHttpActionResult> PUT(StudentDTO studentDTO, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (studentDTO.ID != id)
                return BadRequest(ModelState);

            var flag = await studentService.GetById(id);

            if (flag == null)
                return NotFound();


            try
            {
                var student = mapper.Map<Student>(studentDTO);
                student = await studentService.Update(student);
                return Ok(student);
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }


        /// <summary>
        /// Elimina un objeto a estudiante
        /// </summary>
        /// <remarks>
        /// Elimina el objeto solicitado por ID
        /// </remarks>
        /// <returns>Objeto Eliminado en estudiante </returns>
        /// <response code="200">OK. Elimina El objetos solicitados </response>
        /// <response code="404">Not Found. No se ah Eliminado el objeto solicitado</response>

        [HttpDelete]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            var flag = await studentService.GetById(id);

            if (flag == null)
                return NotFound();

            try
            {
                if (!await studentService.DeleteCheckOnEntity(id))
                    await studentService.Delete(id);
                else
                    throw new Exception("Existe variables relacionadas");

                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }
    }
}
