using Masiv.RouletteProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Business.Utilities
{
    public static class RandomValueGenerator
    {
        public static ColorEnum generateRandomColor()
        {
            ColorEnum[] listColorEnum = new ColorEnum[2];
            listColorEnum[0] = ColorEnum.negro;
            listColorEnum[1] = ColorEnum.rojo;
            Random rnd = new Random();
            int index = rnd.Next(listColorEnum.Length);

            return listColorEnum[index];
        }

        public static int generateRandomNumber()
        {
            int max = 36;
            int min = 0;
            Random rnd = new Random();

            return rnd.Next(min, max + 1);
        }
    }
}
