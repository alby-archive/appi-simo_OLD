namespace AppiSimo.Api.Controllers.Readable
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Shared.Abstract;

    public class EntityController<TEntity>: DefaultController
        where TEntity : class, IEntity, new()
    {
        protected readonly KingRogerContext context;        

        protected EntityController(KingRogerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(Guid id)
        {
            var entity = await context.FindAsync<TEntity>(id);
            return entity;
        }
        
//        [HttpGet]
//        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
//        {
//            context.
//        }
    }
}