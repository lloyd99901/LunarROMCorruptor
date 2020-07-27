# LunarROMCorruptor
[![CodeFactor](https://www.codefactor.io/repository/github/lloyd99901/lunarromcorruptor/badge)](https://www.codefactor.io/repository/github/lloyd99901/lunarromcorruptor)
[![GitHub issues](https://img.shields.io/github/issues/lloyd99901/LunarROMCorruptor)](https://github.com/lloyd99901/LunarROMCorruptor/issues)
[![GitHub stars](https://img.shields.io/github/stars/lloyd99901/LunarROMCorruptor)](https://github.com/lloyd99901/LunarROMCorruptor/stargazers)
[![GitHub license](https://img.shields.io/github/license/lloyd99901/LunarROMCorruptor)](https://github.com/lloyd99901/LunarROMCorruptor/blob/master/LICENSE)

![MainWindow](https://raw.githubusercontent.com/lloyd99901/LunarROMCorruptor/master/static/images/preview.png)

Corrupt any file using multiple different ‘Engines’

---

## About
This project started around 2019 and was originally made in VB.NET. I was fascinated by other corruptors and wanted to make my own. The project was finished at the same year, but since the code was very messy, I decided not to release it. A year later, I decided to remake it in C#, mainly because I want to teach myself more C#, but also to do some code refactoring to make it more optimized.

LunarROMCorruptor uses engines to corrupt files, each engine has its own usage and its own settings that can be changed.
I've included the Nightmare Engine, which is an engine from the Windows Glitch Harvester, into the project so that people that are most familiar with that engine can use it.

![Example](https://raw.githubusercontent.com/lloyd99901/LunarROMCorruptor/master/static/images/fceux_2019-05-08_20-40-56.png)

### Engines
Along with the Nightmare Engine, I've included:
 - Merge Engine
 - Logic Engine
 - Lerp Engine
 - Hell Engine (Not really an engine, just picks a random engine to corrupt the selected byte)
 - Manual (User manually enables and sets what types of corruption takes place in the file.)

## Contributing
Pull requests are welcome. Major and minor.

## Notes
Active development is in the unstable branch of this repo. The master branch is for stable updates.

## License
[MIT](https://choosealicense.com/licenses/mit/)
