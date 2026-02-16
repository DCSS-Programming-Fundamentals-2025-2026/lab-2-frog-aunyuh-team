namespace Shop.Tests
{
    public class InputHelperTests
    {
        [Test]
        public void ParseDecimal_NullInput_Exception()
        {
            var ex = false;

            try
            {
                InputHelper.ParseDecimal(null);
            }
            catch (ArgumentNullException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_TwiceSymbolContains_Exception()
        {
            var ex = false;
            var input = "12.335,92";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_Text_Exception()
        {
            var ex = false;
            var input = "hello,qw qwewpe klllllllllllllllllll";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_Nothing_Exception()
        {
            var ex = false;
            var input = "";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_OnlySymbolPlus_Exception()
        {
            var ex = false;
            var input = "+";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_Overflow_Exception()
        {
            var ex = false;
            var input = "-2923942592348092384932408324908329048328490382092384329483248320482390403284903849849";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_OnlySymbolMinus_Exception()
        {
            var ex = false;
            var input = "-";

            try
            {
                InputHelper.ParseDecimal(input);
            }
            catch (FormatException)
            {
                ex = true;
            }

            Assert.That(ex, Is.True);
        }

        [Test]
        public void ParseDecimal_StartComma_Correct()
        {
            var input = "123,";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(123m));
        }

        [Test]
        public void ParseDecimal_StartPoint_Correct()
        {
            var input = "123.";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(123m));
        }

        [Test]
        public void ParseDecimal_StartComma2_Correct()
        {
            var input = ",123";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(0.123m));
        }

        [Test]
        public void ParseDecimal_StartPoint2_Correct()
        {
            var input = ".123";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);
                                                                     
            Assert.That(result, Is.EqualTo(0.123m));
        }
            
        [Test]
        public void ParseDecimal_ZeroComma_Correct()
        {
            var input = "0,0";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(0m));
        }

        [Test]
        public void ParseDecimal_ZeroPoint_Correct()
        {
            var input = "0.0";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(0m));
        }

        [Test]
        public void ParseDecimal_PlusParse_Correct()
        {
            var input = "+12.3";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(12.3m));
        }

        [Test]
        public void ParseDecimal_MinusParse_Correct()
        {
            var input = "-15.93";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(-15.93m));
        }

        [Test]
        public void ParseDecimal_CommaSymbol_Correct()
        {
            var input = "423,8";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(423.8m));
        }

        [Test]
        public void ParseDecimal_PointSymbol_Correct()
        {
            var input = "321.41";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(321.41m));
        }

        [Test]
        public void ParseDecimal_Simple_Correct()
        {
            var input = "32141";
            var result = 0m;

            result = InputHelper.ParseDecimal(input);

            Assert.That(result, Is.EqualTo(32141m));
        }
    }
}