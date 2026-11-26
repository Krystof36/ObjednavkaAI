using System;
using ObjednavkaAI.Tridy;
using Xunit;

namespace ObjednavkaAI.Tests
{
    public class ObjednavkaTests
    {
        // Způsob dopravy: 0 = Standardní, 1 = Expresní, 2 = Osobní odběr

        [Theory]
        [InlineData(1999, 0)]
        [InlineData(2000, 100)]
        [InlineData(2000.01, 100)]
        [InlineData(4999, 249.95)]
        [InlineData(4999.99, 250)]
        [InlineData(5000, 500)]
        [InlineData(5000.01, 500)]
        [InlineData(7358.75, 735.88)]
        public void VypocitejSlevu_VraciOcekavanouSlevu(double cena, double ocekavanaSleva)
        {
            var objednavka = new Objednavka((decimal)cena, 0);
            Assert.Equal((decimal)ocekavanaSleva, objednavka.VypocitejSlevu());
        }

        [Theory]
        [InlineData(1499, 0, 99)]
        [InlineData(1499.99, 0, 99)]
        [InlineData(1500, 0, 0)]
        [InlineData(2000, 1, 199)]
        [InlineData(5000, 1, 199)]
        [InlineData(1000, 2, 0)]
        public void VypocitejDopravu_VraciOcekavanouDopravu(double cena, int doprava, double ocekavanaDoprava)
        {
            var objednavka = new Objednavka((decimal)cena, doprava);
            Assert.Equal((decimal)ocekavanaDoprava, objednavka.VypocitejDopravu());
        }

        [Theory]
        [InlineData(0, 2, 0)]
        [InlineData(1499, 0, 1598)]
        [InlineData(2000, 0, 1900)]
        [InlineData(5000, 1, 4699)]
        [InlineData(1499.99, 0, 1598.99)]
        [InlineData(3847.60, 1, 3854.22)]
        [InlineData(7358.75, 0, 6622.87)]
        [InlineData(12496.80, 1, 11346.12)]
        public void VypocitejCelkem_VraciOcekavanouCelkovouCenu(double cena, int doprava, double ocekavanaCelkovaCena)
        {
            var objednavka = new Objednavka((decimal)cena, doprava);
            Assert.Equal((decimal)ocekavanaCelkovaCena, objednavka.VypocitejCelkem());
        }

        [Fact]
        public void Konstruktor_ZapornaCena_VyhodiVyjimku()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Objednavka(-0.01m, 0));
        }

        [Fact]
        public void Konstruktor_NeplatnaDoprava_VyhodiVyjimku()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Objednavka(1000m, 3));
        }
    }
}
