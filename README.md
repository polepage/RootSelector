# Root Faction Selector

Root Faction Selector is an Android app to select factions, seating order and first player of the boardgame Root by [Leder Games](https://ledergames.com/).

* [Informations for Players](#for-players)
* [Informations for Contributors](#for-contributors)

## For Players

### Setup

Currently, the app is only available as an apk to side-load in your Android device. If demand is there ([by adding/commenting an issue here](https://github.com/polepage/RootSelector/issues)), I'll try to add it on the Play Store. Also, I'm not interested in doing an iOS version, again file an [issue](https://github.com/polepage/RootSelector/issues) or [do it yourself](#for-contributors).

* [The APK file](https://drive.google.com/file/d/100RW20Bt3qn7554yYil93uQg6xFW0FsG/view?usp=sharing)
* [Questions/Issues](https://github.com/polepage/RootSelector/issues)

### Usage

To setup a game, sit around the table in whatever way you choose. Then, a player with the app will setup the game.

Setup screen:

![Setup Screen](/wiki/setup.png)

1. Payer Count: This is self-explanatory.
2. Target Reach: This is the total Reach you want the factions in your game to get to. Default value is the value recommanded in the rulebook for your player count. You can select any value between 17 and the maximum for your player count.
3. The table display the factions that will be part of  the random selection. Factions that are mathematically impossible to use for a given target reach will be removed.
4. In the table you can exclude specific factions from the algorithm.
5. Hit the continue button to generate a game.

Notes:
* Removing "Vagabond" will also exclude "Second Vagabond" even if it is not explicit.
* If you remove factions such as there is not enough factions for the number of player, there will be an error message and you won't be able to generate a game.
* If you remove factions such as it becomes impossible to reach 17 (the minimum in the rulebook), there will be an error message and you won't be able to generate a game.
* If you remove factions such as it becomes impossible to reach your target, there will be a warning message and if you start a game in that state the maximum of the available factions will be the target reach.

Result screen:

![Result Screen](/wiki/results.png)

1. The table list the factions to distribute to each player around the table, starting with the player holding the device where the game is setup.
2. The player that gets the highlighted faction will be the first player.

Notes:
* Using the back button will destroy the displayed game. The settings will stay the same and you can regenerate a new game with the same parameters.

Links:
* [Questions/Issues/Feature Requests](https://github.com/polepage/RootSelector/issues)

## For Contributors

* The application is built in VS2019, using Xamarin.Android and .NET Standard 2.1.
* If you have an issue or features request, go [here](https://github.com/polepage/RootSelector/issues)
* If you can solve the issue or add the feature yourself, feel free to do so and start a pull request.
* If you are interested in using the rules library to build a Xamarin.iOS version, have fun.
