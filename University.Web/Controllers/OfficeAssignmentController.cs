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
    public class OfficeAssignmentController : ApiController
    {

        private IMapper mapper;
        private readonly OfficeAssignmentService officeAssignmentService = new OfficeAssignmentService(new OfficeAssignmentRepository(UniversityContext.Create()));

        public OfficeAssignmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// obtiene los objetos de oficina de asginamiento
        /// </summary>
        /// <returns>  listado de los objetos de oficina de asginamiento  </returns>
        ///  <response code="200">OK. Devuleve el listado de objetos solicitados </response>



        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var officeAssignments = await officeAssignmentService.GetAll();
            var officeAssignmentsDTO = officeAssignments.Select(x => mapper.Map<OfficeAssignmentDTO>(x));

            return Ok(officeAssignmentsDTO);
        }

        /// <summary>
        ///  Obtiene un objeto oficina de asginamiento por su ID
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su ID.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto oficina de asginamiento </returns>
        /// <response code="200">OK. Devuleve el objetos solicitados </response>
        /// <response code="404">Not Found. No se ah encontrado el objeto solicitado </response>


        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var officeAssignment = await officeAssignmentService.GetById(id);

            if (officeAssignment == null)
                return NotFound();


            var officeAssignmentDTO = mapper.Map<OfficeAssignmentDTO>(officeAssignment);

            return Ok(officeAssignmentDTO);
        }

        /// <summary>
        /// Inserta un objeto a oficina de asginamiento
        /// </summary>
        /// <remarks>
        /// Todos los campos son necesarios llenar
        /// </remarks>
        /// <returns>Objeto Insertado en oficina de asginamiento</returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>


        [HttpPost]
        public async Task<IHttpActionResult> Post(OfficeAssignmentDTO officeAssignmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentService.Insert(officeAssignment);
                return Ok(officeAssignmentDTO);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }


        /// <summary>
        /// Actualiza un objeto a oficina de asginamiento
        /// </summary>
        /// <remarks>
        /// Actualizar los campos deseados
        /// </remarks>
        /// <returns>Objeto Actualizado en oficina de asginamiento </returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>
        /// <response code="404">Not Found. No se ah Actualizado el objeto solicitado</response>


        [HttpPut]
        public async Task<IHttpActionResult> PUT(OfficeAssignmentDTO officeAssignmentDTO, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (officeAssignmentDTO.InstructorID != id)
                return BadRequest(ModelState);

            var flag = await officeAssignmentService.GetById(id);

            if (flag == null)
                return NotFound();


            try
            {
                var officeAssignment = mapper.Map<OfficeAssignment>(officeAssignmentDTO);
                officeAssignment = await officeAssignmentService.Update(officeAssignment);
                return Ok(officeAssignment);
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }



        /// <summary>
        /// Elimina un objeto a oficina de asginamiento
        /// </summary>
        /// <remarks>
        /// Elimina el objeto solicitado por ID
        /// </remarks>
        /// <returns>Objeto Eliminado en oficina de asginamiento </returns>
        /// <response code="200">OK. Elimina El objetos solicitados </response>
        /// <response code="404">Not Found. No se ah Eliminado el objeto solicitado</response>


        [HttpDelete]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            var flag = await officeAssignmentService.GetById(id);
            
            if (flag == null)
                return NotFound();

            try
            {
                if (!await officeAssignmentService.DeleteCheckOnEntity(id))
                    await officeAssignmentService.Delete(id);
                else
                    throw new Exception("Existe variables relacionadas");

                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }

    }
}
