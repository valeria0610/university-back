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
    public class DepartmentController : ApiController
    {

        private IMapper mapper;
        private readonly DepartmentService departmentService = new DepartmentService(new DepartmentRepository(UniversityContext.Create()));

        public DepartmentController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// obtiene los objetos de departamento 
        /// </summary>
        /// <returns>  listado de los objetos de departamento  </returns>
        ///  <response code="200">OK. Devuleve el listado de objetos solicitados </response>



        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var departments = await departmentService.GetAll();
            var departmentsDTO = departments.Select(x => mapper.Map<DepartmentDTO>(x));

            return Ok(departmentsDTO);
        }

        /// <summary>
        ///  Obtiene un objeto departamento  por su ID
        /// </summary>
        /// <remarks>
        /// Obtiene un objeto por su ID.
        /// </remarks>
        /// <param name="id">Id del objeto</param>
        /// <returns>Objeto departamento </returns>
        /// <response code="200">OK. Devuleve el objetos solicitados </response>
        /// <response code="404">Not Found. No se ah encontrado el objeto solicitado </response>


        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var department = await departmentService.GetById(id);

            if (department == null)
                return NotFound();


            var departmentDTO = mapper.Map<DepartmentDTO>(department);

            return Ok(departmentDTO);
        }



        /// <summary>
        /// Inserta un objeto a departamento 
        /// </summary>
        /// <remarks>
        /// Todos los campos son necesarios llenar
        /// </remarks>
        /// <returns>Objeto Insertado en departamento</returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>


        [HttpPost]
        public async Task<IHttpActionResult> Post(DepartmentDTO departmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var department = mapper.Map<Department>(departmentDTO);
                department = await departmentService.Insert(department);
                return Ok(department);
            }
            catch (Exception ex) { return InternalServerError(ex); }
        }

        /// <summary>
        /// Actualiza un objeto a departamento 
        /// </summary>
        /// <remarks>
        /// Actualizar los campos deseados
        /// </remarks>
        /// <returns>Objeto Actualizado en departamento </returns>
        /// <response code="200">OK. Inserta los objetos solicitados </response>
        /// <response code="400">Bad Request. No se ah Insertado el objeto solicitado </response>
        /// <response code="404">Not Found. No se ah Actualizado el objeto solicitado</response>


        [HttpPut]
        public async Task<IHttpActionResult> PUT(DepartmentDTO departmentDTO, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (departmentDTO.DepartmentID != id)
                return BadRequest(ModelState);

            var flag = await departmentService.GetById(id);

            if (flag == null)
                return NotFound();


            try
            {
                var department = mapper.Map<Department>(departmentDTO);
                department = await departmentService.Update(department);
                return Ok(department);
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }



        /// <summary>
        /// Elimina un objeto a departamento 
        /// </summary>
        /// <remarks>
        /// Elimina el objeto solicitado por ID
        /// </remarks>
        /// <returns>Objeto Eliminado en departamento </returns>
        /// <response code="200">OK. Elimina El objetos solicitados </response>
        /// <response code="404">Not Found. No se ah Eliminado el objeto solicitado</response>


        [HttpDelete]
        public async Task<IHttpActionResult> DELETE(int id)
        {
            var flag = await departmentService.GetById(id);

            if (flag == null)
                return NotFound();

            try
            {
                if (!await departmentService.DeleteCheckOnEntity(id))
                    await departmentService.Delete(id);
                else
                    throw new Exception("Existe variables relacionadas");

                return Ok();
            }
            catch (Exception ex) { return InternalServerError(ex); }

        }

    }
}
