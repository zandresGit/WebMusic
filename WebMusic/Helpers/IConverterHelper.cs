using System;
using WebMusic.Models;

namespace WebMusic.Helpers
{
    public interface IConverterHelper
    {
        Album ToAlbum(AlbumViewModel model, Guid imageId, bool isNew);

        AlbumViewModel ToAlbumViewModel(Album album);
    }

}
