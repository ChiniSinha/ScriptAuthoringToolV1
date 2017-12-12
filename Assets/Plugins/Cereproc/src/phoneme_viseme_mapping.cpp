#include "phoneme_viseme_mapping.h"
#include <string>

void PhonemeToViseme(std::string& phoneme)
{
	if (phoneme == "sil")
	{
		phoneme = "0";
		return;
	}

	if (phoneme == "aa")
	{
		phoneme = "2";
		return;
	}

	if (phoneme == "ae")
	{
		phoneme = "1";
		return;
	}

	if (phoneme == "ah")
	{
		phoneme = "1";
		return;
	}

	if (phoneme == "ao")
	{
		phoneme = "3";
		return;
	}

	if (phoneme == "aw")
	{
		phoneme = "9";
		return;
	}

	if (phoneme == "o")
	{
		phoneme = "9";
		return;
	}

	if (phoneme == "ax")
	{
		phoneme = "1";
		return;
	}

	if (phoneme == "ay")
	{
		phoneme = "11";
		return;
	}

	if (phoneme == "b")
	{
		phoneme = "21";
		return;
	}

	if (phoneme == "ch")
	{
		phoneme = "16";
		return;
	}

	if (phoneme == "d")
	{
		phoneme = "19";
		return;
	}

	if (phoneme == "dx")
	{
		phoneme = "19";
		return;
	}

	if (phoneme == "dh")
	{
		phoneme = "17";
		return;
	}

	if (phoneme == "eh")
	{
		phoneme = "4";
		return;
	}

	if (phoneme == "e")
	{
		phoneme = "4";
		return;
	}

	if (phoneme == "@")
	{
		phoneme = "4";
		return;
	}

	if (phoneme == "er")
	{
		phoneme = "5";
		return;
	}

	if (phoneme == "ey")
	{
		phoneme = "4";
		return;
	}

	if (phoneme == "f")
	{
		phoneme = "18";
		return;
	}

	if (phoneme == "g")
	{
		phoneme = "20";
		return;
	}

	if (phoneme == "hh")
	{
		phoneme = "12";
		return;
	}

	if (phoneme == "h")
	{
		phoneme = "12";
		return;
	}

	if (phoneme == "ih")
	{
		phoneme = "6";
		return;
	}

	if (phoneme == "i")
	{
		phoneme = "6";
		return;
	}

	if (phoneme == "iy")
	{
		phoneme = "6";
		return;
	}

	if (phoneme == "jh")
	{
		phoneme = "16";
		return;
	}

	if (phoneme == "k")
	{
		phoneme = "20";
		return;
	}

	if (phoneme == "l")
	{
		phoneme = "14";
		return;
	}

	if (phoneme == "m")
	{
		phoneme = "21";
		return;
	}

	if (phoneme == "n")
	{
		phoneme = "19";
		return;
	}

	if (phoneme == "ng")
	{
		phoneme = "20";
		return;
	}

	if (phoneme == "ow")
	{
		phoneme = "8";
		return;
	}

	if (phoneme == "ou")
	{
		phoneme = "8";
		return;
	}

	if (phoneme == "oy")
	{
		phoneme = "10";
		return;
	}

	if (phoneme == "p")
	{
		phoneme = "21";
		return;
	}

	if (phoneme == "r")
	{
		phoneme = "13";
		return;
	}

	if (phoneme == "s")
	{
		phoneme = "15";
		return;
	}

	if (phoneme == "sh")
	{
		phoneme = "16";
		return;
	}

	if (phoneme == "t")
	{
		phoneme = "19";
		return;
	}

	if (phoneme == "th")
	{
		phoneme = "17";
		return;
	}

	if (phoneme == "uh")
	{
		phoneme = "4";
		return;
	}

	if (phoneme == "uw")
	{
		phoneme = "7";
		return;
	}

	if (phoneme == "uu")
	{
		phoneme = "7";
		return;
	}

	if (phoneme == "v")
	{
		phoneme = "18";
		return;
	}

	if (phoneme == "w")
	{
		phoneme = "7";
		return;
	}

	if (phoneme == "y")
	{
		phoneme = "6";
		return;
	}

	if (phoneme == "z")
	{
		phoneme = "15";
		return;
	}

	if (phoneme == "zh")
	{
		phoneme = "16";
		return;
	}

	if (phoneme == "R")
	{
		phoneme = "13";
		return;
	}

	phoneme = "-1";
}
