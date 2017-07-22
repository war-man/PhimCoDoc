﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Data;
using Microsoft.EntityFrameworkCore;

namespace DVMN.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmRepository _repository;
        private readonly ApplicationDbContext _context;

        public FilmController(IFilmRepository repository,
            ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Route("/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            ViewData["FilmDetials"] = await _repository.Get(slug,"1");
            ViewData["SingleRightFilm"] = await _repository.GetSingleRightFilms();
            ViewData["bannerBottomFilm"] = _repository.GetBannerBottomFilm();
            return View();
        }
        [Route("/xem-phim/{slug}")]
        public async Task<IActionResult> Watch(string server,string slug)
        {
            if (String.IsNullOrEmpty(server))
                server = "1";
            ViewData["FilmDetials"] = await _repository.Get(slug, server);
            ViewData["bannerBottomFilm"] = await _repository.GetBannerBottomFilm();
            await _repository.IsWatched(slug);
            return View();
        }
        
        [Route("/tai-phim/{slug}")]
        public async Task<IActionResult> Download(string slug)
        {
            ViewData["FilmDetials"] = await _repository.GetDownloadFilm(slug);
            ViewData["SingleRightFilm"] = await _repository.GetSingleRightFilms();
            ViewData["bannerBottomFilm"] = await _repository.GetBannerBottomFilm();
            return View();
        }

    }
}