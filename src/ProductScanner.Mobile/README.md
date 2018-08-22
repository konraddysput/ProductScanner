# Product Scanner Android Application #
Mobile application that allows user to capture picture and send it to product scanner API and execute advanced recognition rules.

# Author #
Konrad Dysput (konrad.dysput@gmail.com)

# Technologies #
* TypeScript,
* Ionic,
* Cordova

# Getting started #
To capture pictures you have to add support for mobile devices - iPhone or Android. Execute commands from below to run application

```bash
$ cd to current dictory
$ npm install -g ionic
$ npm install -g cordova
$ npm install
$ ionic servce
```

To add support for android devices:
```bash
$ cordova add platform android
$ ionic cordova build android
```

To add support for iPhone:
```bash
$ cordova add platform ios
$ ionic cordova build ios
```

To debug application on your mobile phone, execute following command:
```bash
ionic cordova run <ios or android> --device -l --debug
```