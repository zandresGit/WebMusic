using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMusic.Data;
using WebMusic.Models;

public class SeedDb
{
    private readonly DataContext _context;
    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckGenerosAsync();

    }
    private async Task CheckGenerosAsync()
    {
        if (!_context.Generos.Any())
        {
            _context.Generos.Add(new Genero
            {
                des_genero = "Metal",
                Bandas = new List<Banda>
                {
                    new Banda
                    {
                        nombre = "Metallica",
                        origen = "Estados Unidos",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "And Justice For All",
                                anio = 1988,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "One",
                                       Link = "www.youtube.com/watch?v=WM8bTdBs-cw",
                                       Duracion = "6:00"
                                   }
                                  
                                }
                                
                            }
                            
                        }
                    },
                    new Banda
                    {
                        nombre = "Megadeth",
                        origen = "Estados Unidos",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "Countdown to Extinction",
                                anio = 1992,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "Symphony Of Destruction" ,
                                       Link = "www.youtube.com/watch?v=K5jvUXij7nU",
                                       Duracion = "5:00" 
                                   }
                                }

                            }

                        }
                    },
                     new Banda
                    {
                        nombre = "Slipknot",
                        origen = "Estados Unidos",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "All Hope Is Gone",
                                anio = 2008,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "Gematria",
                                       Link = "www.youtube.com/watch?v=mSDwgb8ZIyk",
                                       Duracion = "6:00" 
                                   }
                                }

                            }

                        }
                    }
                }
            });
            _context.Generos.Add(new Genero
            {
                des_genero = "Rock",
                Bandas = new List<Banda>
                {
                    new Banda
                    {
                        nombre = "Red Hot Chili Papers",
                        origen = "Estados Unidos",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "One Hot Minute",
                                anio = 1995,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "Warped",
                                       Link = "www.youtube.com/watch?v=xmyuJZH3RAc",
                                       Duracion = "4:00" 
                                   }
                                }

                            }

                        }
                    },
                    new Banda
                    {
                        nombre = "The Beatles",
                        origen = "Reino Unido",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "Please Please Me",
                                anio = 1963,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "Love Me Do",
                                       Link = "www.youtube.com/watch?v=0pGOFX1D_jg",
                                       Duracion = "2:00" 
                                   }
                                }

                            }

                        }
                    },
                     new Banda
                    {
                        nombre = "Green Day",
                        origen = "Estados Unidos",
                        Albums = new List<Album>
                        {
                            new Album
                            {
                                nombre = "American Idiot",
                                anio = 2004,
                                Cancions = new List<Cancion>
                                {
                                   new Cancion 
                                   { 
                                       Nombre = "Jesus of Suburbia",
                                       Link = "www.youtube.com/watch?v=XrHjKN2bjb0",
                                       Duracion = "9:00" 
                                   }
                                }

                            }

                        }
                    }
                }
            });
            await _context.SaveChangesAsync();
        }


    }
}