﻿namespace AppiSimo.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstract;

    public class EntityController<TEntity> : DefaultController
        where TEntity : class, IEntity, new()
    {
        readonly KingRogerContext _context;

        protected EntityController(KingRogerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get() => await _context.Set<TEntity>().ToListAsync();

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            TEntity entity;

            try
            {
                entity = await _context.FindAsync<TEntity>(id);
            }
            catch
            {
                return new TEntity();
            }
            
            return entity;
        }

        [HttpPost]
        public async Task<HttpStatusCode> Post([FromBody] TEntity entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.Created;
        }

        [HttpPut]
        public async Task<HttpStatusCode> Put(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
               
                await _context.SaveChangesAsync();
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }

        [HttpDelete("{id}")]
        public virtual async Task<HttpStatusCode> Delete(Guid id)
        {
            var entity = new TEntity
            {
                Id = id
            };

            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }

            return HttpStatusCode.OK;
        }
    }
}