using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Controllers
{
    public class edgesController : ApiController
    {
        private ModelContext db = new ModelContext();

        // GET: api/edges
        public IQueryable<Edge> GetEdges()
        {
            return db.Edges;
        }

        // GET: api/edges/5
        [ResponseType(typeof(Edge))]
        public IHttpActionResult GetEdge(int id)
        {
            Edge edge = db.Edges.Find(id);
            if (edge == null)
            {
                return NotFound();
            }

            return Ok(edge);
        }

        // PUT: api/edges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEdge(int id, Edge edge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != edge.PlaceMarkId1)
            {
                return BadRequest();
            }

            db.Entry(edge).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EdgeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/edges
        [ResponseType(typeof(Edge))]
        public IHttpActionResult PostEdge(Edge edge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Edges.Add(edge);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EdgeExists(edge.PlaceMarkId1))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = edge.PlaceMarkId1 }, edge);
        }

        // DELETE: api/edges/5
        [ResponseType(typeof(Edge))]
        public IHttpActionResult DeleteEdge(int id)
        {
            Edge edge = db.Edges.Find(id);
            if (edge == null)
            {
                return NotFound();
            }

            db.Edges.Remove(edge);
            db.SaveChanges();

            return Ok(edge);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EdgeExists(int id)
        {
            return db.Edges.Count(e => e.PlaceMarkId1 == id) > 0;
        }
    }
}