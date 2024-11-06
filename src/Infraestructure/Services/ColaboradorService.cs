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

namespace Infraestructure.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly ApplicationDbContext _context;

        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> GetListColaboradores(DateTime fechaInicio, DateTime fechaFin, bool esProfesor)
        {
            Response<object> results = new();
            try
            {
                string sql = "SELECT * FROM Tbl_Colaboradores WHERE fechaCreacion BETWEEN @fechaInicio AND @fechaFin AND esProfesor = @esProfesor";
                var response = await _context.Database.GetDbConnection().QueryAsync(sql, new { fechaInicio, fechaFin, esProfesor });

                results.Succeeded = true;
                results.Message = "Listado de Colaboradores";
                results.Result = response;

                return results;
            }
            catch (Exception ex)
            {
                results.Succeeded = false;
                results.Message = $"Ocurrio un error + {ex}";

                return results;
            }
        }

        public async Task<Response<object>> PostColaborador(Colaboradores colaboradores)
        {
            Response<object> results = new();
            try
            {
                var guardarColaborador = await _context.Colaboradores.AddAsync(colaboradores);

                await _context.SaveChangesAsync();

                if(colaboradores.esProfesor == true)
                {
                    var profesor = new Profesor
                    {
                        FkColaborador = colaboradores.id
                    };

                    var guardarProfesor = _context.Profesores.AddAsync(profesor);

                    await _context.SaveChangesAsync();
                } else
                {
                    var admistrativo = new Administrativo
                    {
                        FkColaborador = colaboradores.id
                    };

                    var guardarAdministrativo = await _context.Administrativos.AddAsync(admistrativo);

                    await _context.SaveChangesAsync();
                }

                results.Succeeded = true;
                results.Message = "Guardado con Exito el Colaborador";
                results.Result = colaboradores;

                return results;
            }
            catch (Exception ex)
            {
                results.Succeeded = false;
                results.Message = $"Ha ocurrido un error {ex}";
                results.Result = ex;

                return results;
            }
        }
    }
}