using System;

namespace ObjednavkaAI.Tridy
{
    public class Objednavka
    {
        public decimal CenaZbozi { get; }
        public int ZpusobDopravy { get; } // 0 = Standardní, 1 = Expresní, 2 = Osobní odběr

        public Objednavka(decimal cenaZbozi, int zpusobDopravy)
        {
            if (cenaZbozi < 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(cenaZbozi), "Záporná cena zboží není povolena.");
            }

            if (zpusobDopravy < 0 || zpusobDopravy > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(zpusobDopravy), "Neplatný způsob dopravy.");
            }

            CenaZbozi = cenaZbozi;
            ZpusobDopravy = zpusobDopravy;
        }

        public decimal VypocitejSlevu()
        {
            decimal sazba = 0m;

            if (CenaZbozi >= 5000m)
            {
                sazba = 0.10m;
            }
            else if (CenaZbozi >= 2000m)
            {
                sazba = 0.05m;
            }

            return Math.Round(CenaZbozi * sazba, 2, MidpointRounding.AwayFromZero);
        }

        public decimal VypocitejDopravu()
        {
            return ZpusobDopravy switch
            {
                0 => CenaZbozi >= 1500m ? 0m : 99m,
                1 => 199m,
                2 => 0m,
                _ => throw new ArgumentOutOfRangeException(nameof(ZpusobDopravy), "Neplatný způsob dopravy.")
            };
        }

        public decimal VypocitejCelkem()
        {
            return CenaZbozi - VypocitejSlevu() + VypocitejDopravu();
        }
    }
}
