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
    public class InstructorController : ApiController
    {

        private IMapper mapper;
        private readonly InstructorService instructorService = new InstructorService(new InstructorRepository(UniversityContext.Create()));

        public InstructorController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// obtiene los objetos de instructor
        /// </summary>
        /// <returns>  listado de los objetos de instructor  </returns>
        ///  <response code="200">OK. Devuleve el listado de objetos solicitados </response>



        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var instructores = await instructorService.GetAll();
            var instructoresDTO = instructores.Select(x => mapper.Map<InstructorDTO>(x));

            return Ok(instructoresDTO);
        }

        /// <summary>
        ///  Obtiene un objeto instructor por su ID
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su ID.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto instructor </returns>
        /// <response code="200">OK. Devuleve el objetos solicitados </response>
        /// <response code="404">Not Found. No se ah encontrado el objeto solicitado </response>



        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var instructor = await instructorService.GetById(id);

            if (instructor == null)
                return NotFound();


            var instructorDTO = mapper.Map<InstructorDTO>(instructor);

            return Ok(instructorDTO);
        }


        /// <summary>
        /// Inserta un objeto a instructor
        /// </summary>
        /// <remarks>
        /// Todos los campos son necesarios llenar
        /// </remarks>
        /// <returns>Objeto Insertado en instructor</returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>

        [HttpPost]
        public async Task<IHttpActionResult> Post(InstructorDTO instructorDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Insert(instructor);
                return Ok(instructor);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        /// <summary>
        /// Actualiza un objeto a instructor
        /// </summary>
        /// <remarks>
        /// Actualizar los campos deseados
        /// </remarks>
        /// <returns>Objeto Actualizado en instructor </returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>
        /// <response code="404">Not Found. No se ah Actualizado el objeto solicitado</response>

        [HttpPut]
        public async Task<IHttpActionResult> PUT(InstructorDTO instructorDTO, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (instructorDTO.ID != id)
                return BadRequest(ModelState);

            var flag = await instructorService.GetById(id);

            if (flag == null)
                return NotFound();


            try
            {
                var instructor = mapper.Map<Instructor>(instructorDTO);
                instructor = await instructorService.Update(instructor);
                return Ok(instructor);
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }


        /// <summary>
        /// Elimina un objeto a instructor
        /// </summary>
        /// <remarks>
        /// Elimina el objeto solicitado por ID
        /// </remarks>
        /// <returns>Objeto Eliminado en instructor </returns>
        /// <response code="200">OK. Elimina El objetos solicitados </response>
        /// <response code="404">Not Found. No se ah Eliminado el objeto solicitado</response>



        [HttpDelete]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            var flag = await instructorService.GetById(id);

            if (flag == null)
                return NotFound();

            try
            {
                if (!await instructorService.DeleteCheckOnEntity(id))
                    await instructorService.Delete(id);
                else
                    throw new Exception("Existe variables relacionadas");

                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }
    }
}
