using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OData_Basic.Context;
using OData_Basic.Models;

namespace OData_Basic.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
         private readonly MusicContext _context;
        public AlbumsController(MusicContext context)
        {
            _context = context;
            if (!context.Albums.Any())
            {
                SeedData();
            }
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            var albums = _context.Albums;
            return Ok(albums);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public IActionResult Get(int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var album = _context.Albums.Include(a => a.Songs).Where(a => a.Id == key);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(album);
        }

        private void SeedData()
        {
            for (int i = 0; i < 10; i++)
            {
                var album = new Album
                {
                    Name = $"Test {i}"
                };

                _context.Albums.Add(album);

                _context.SaveChanges();
            }

            var albums = _context.Albums;

            foreach (var album in albums)
            {
                var song1 = new Song
                {
                    Album= album,
                    Name = $"song {new Random().Next(1000, 9999)}"
                };

                var song2 = new Song
                {
                    Album = album,
                    Name = $"song {new Random().Next(1000, 9999)}"
                };

                var song3 = new Song
                {
                    Album = album,
                    Name = $"song {new Random().Next(1000, 9999)}"
                };

                _context.Songs.Add(song1);
                _context.Songs.Add(song2);
                _context.Songs.Add(song3);

                _context.SaveChanges();
            }
        }
    }
}
