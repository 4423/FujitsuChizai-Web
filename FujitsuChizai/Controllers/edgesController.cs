﻿using System;
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
using FujitsuChizai.Models;

namespace FujitsuChizai.Controllers
{
    public class edgesController : ApiController
    {
        private ModelContext db = new ModelContext();
        private IRouteFinding rf = RouteFinding.Instance;

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

            rf.RequestInitialization();

            return CreatedAtRoute("DefaultApi", new { id = edge.PlaceMarkId1 }, edge);
        }

        // DELETE: api/edges/5
        [ResponseType(typeof(Edge))]
        public IHttpActionResult DeleteEdge(int id1, int id2)
        {
            Edge edge = db.Edges.SingleOrDefault(x => x.PlaceMarkId1 == id1 && x.PlaceMarkId2 == id2);
            if (edge == null)
            {
                return NotFound();
            }

            db.Edges.Remove(edge);
            db.SaveChanges();

            rf.RequestInitialization();

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