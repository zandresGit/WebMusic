using System;
using WebMusic.Models;

namespace WebMusic.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Album ToAlbum(AlbumViewModel model, Guid imageId, bool isNew)
        {
            return new Album
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
                nombre = model.nombre
            };
        }

        public AlbumViewModel ToAlbumViewModel(Album album)
        {
            return new AlbumViewModel
            {
                Id = album.Id,
                ImageId = album.ImageId,
                nombre = album.nombre
            };
        }
    }

}
