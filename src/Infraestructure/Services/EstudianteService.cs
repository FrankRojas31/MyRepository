using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Dapper;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.DataAccess.ObjectBinding;

namespace Infraestructure.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;

        public EstudianteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> ObtenerDatos()
        {
            var data = await _context.Estudiantes.ToListAsync();
            return new Response<object>(data, "Encontre Datos de la tabla de Estudiante");
        }

        public async Task<Response<EstudianteDTO>> InsertarDatos(EstudianteDTO estudianteDTO)
        {
            var estudiante = new Estudiantes
            {
                Id = estudianteDTO.Id,
                nombre = estudianteDTO.nombre,
                edad = estudianteDTO.edad,
                correo = estudianteDTO.correo,
                IsDeleted = false
            };

            await _context.Estudiantes.AddAsync(estudiante);

            await _context.SaveChangesAsync();

            var response = new Response<EstudianteDTO>
            {
                Succeeded = true,
                Message = "Estudiante agregado exitosamente",
                Result = estudianteDTO
            };

            return response;
        }

        public async Task<Response<object>> ObtenerDatoPorId(int id)
        {
            var result = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);

            if(result is null)
            {
                var response = new Response<object>
                {
                    Succeeded = false,
                    Message = "El estudiante no existe",
                };
                return response;
            } else {
            var response = new Response<object>
            {
                Succeeded = true,
                Message = "Estudiante encontrado",
                Result = new EstudianteDTO
                {
                    Id = result.Id,
                    nombre = result.nombre,
                    edad = (int)result.edad,
                    correo = result.correo
                }
            };
                return response;
            }
        }

        public async Task<Response<object>> ActualizarDato(int id, EstudianteDTO estudianteDTO)
        {
            var result = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);

            if(result is not null)
            {
                if(estudianteDTO.nombre is not null)
                {
                    result.nombre = estudianteDTO.nombre;
                }

                if(estudianteDTO.correo is not null)
                {
                    result.correo = estudianteDTO.correo;
                }

                if(estudianteDTO.edad is not null)
                {
                    result.edad = estudianteDTO.edad;
                }

                if (estudianteDTO.IsDeleted is not null)
                {
                    result.IsDeleted = estudianteDTO.IsDeleted;
                }

                await _context.SaveChangesAsync();

                var results = new Response<object>
                {
                    Succeeded = true,
                    Message = "Se ha actualizado con exito",
                    Result = result
                };
                return results;
            } else
            {
                var results = new Response<object>
                {
                    Succeeded = false,
                    Message = "No se ha actualizado",
                    Result = new string[0]
                };
                return results;
            }
        }

        public async Task<Response<object>> EliminarDato(int id, bool BorradoLogico)
        {
            Response<object> response = new Response<object>();
            var result = await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                if (result is not null)
                {
                    if (BorradoLogico)
                    {
                        result.IsDeleted = true;
                        _context.Estudiantes.Update(result);
                        await _context.SaveChangesAsync();

                        response.Message = "Se ha borrado logicamente con exito";
                        response.Result = result;
                        response.Succeeded = true;

                        return response;
                    }
                    else
                    {
                        _context.Estudiantes.Remove(result);
                        await _context.SaveChangesAsync();

                        response.Message = "Se ha borrado fisicamente con exito";
                        response.Result = result;
                        response.Succeeded = true;

                        return response;
                    }
                }
                else
                {
                    response.Message = "No se encontró el estudiante";
                    response.Result = new string[0];
                    response.Succeeded = false;
                }
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
                response.Result = new string[0];
            }

            return response;
        }

        public async Task<byte[]> GetPDF()
        {
            ObjectDataSource source = new ObjectDataSource();
            source.DataSource = typeof(EstudiantePDFDTO);
            source.DataMember = "Estudiantes";

            var report = new ApplicationCore.PDF.Estudiantes_Reporte();
            report.DataSource = source;

            var estudiantes = await (from e in _context.Estudiantes
                                     select new EstudianteDTO
                                     {
                                         Id = e.Id,
                                         edad = e.edad,
                                         nombre = e.nombre,
                                         correo = e.correo
                                     }).ToListAsync();


            EstudiantePDFDTO reportepdf = new EstudiantePDFDTO
            {
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                Hora = DateTime.Now.ToString("HH:mm"),
                Estudiantes = estudiantes
            };

            source.DataSource = reportepdf;

            using (var memory = new MemoryStream())
            {
                await report.ExportToPdfAsync(memory);
                memory.Position = 0;
                return memory.ToArray();
            }
        }

    }
}