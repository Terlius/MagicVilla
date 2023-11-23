using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO{Id= 1, Name = "Villa 1"},
            new VillaDTO{Id= 2, Name = "Villa 2"}
        };

    }
}
