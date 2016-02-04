/**
 * https://groups.google.com/forum/#!topic/reactnative/vrdcrjh-jX0
 * https://github.com/facebook/react-native/issues/4577
 * This file patches react-native's packager's .babelrc file to add support
 * for decorators. There's no way to subclass and extend the .babelrc file,
 * as such we have to directly modify it.
 */

const path = require('path');
const fs = require('fs');

// Path to react-native's .babelrc file.
// This value can change as we update react-native.
const babelRcPath = path.resolve(
  __dirname,
  './node_modules/react-native/',
  'packager/react-packager/.babelrc'
);


const babelRc = fs.readFileSync(babelRcPath, 'utf8');
const babelRcJson = JSON.parse(babelRc);

// Decorator plugin we want to add to react-native's packager.
const decoratorPlugin = 'transform-decorators-legacy';

// If we haven't already modified the .babelrc file then do it now.
if (babelRcJson.plugins[0] !== decoratorPlugin) {
  console.log('Patched react-native packager .babelrc');

  // Prepend.
  babelRcJson.plugins.unshift(decoratorPlugin);

  // Write file.
  fs.writeFileSync(
    babelRcPath,
    JSON.stringify(babelRcJson),
    'utf8'
  );
}



/* remove other .babelrc */
const uselessBabelRcPath = path.resolve(
  __dirname,
  './node_modules/cuz-native/',
  'node_modules/redux-request/',
  '.babelrc'
);
fs.unlink(uselessBabelRcPath, (err) => {
  if (err) throw err;
  console.log('Removed uesless .babelrc');
});



/* remove dump files. */
[
  path.resolve( __dirname, './node_modules/', 'react-native/node_modules/react'),
  path.resolve( __dirname, './node_modules/', 'react-native-mock/node_modules/react-native'),
  path.resolve( __dirname, './node_modules/', 'react-native-mock/node_modules/react'),
  path.resolve( __dirname, './node_modules/', 'react-native/node_modules/fbjs'),
].forEach((dir) => {
  require('child_process').exec('rm -rf ' + dir, (err, out) => {
    if (err) throw err;
    console.log('Removed dump ' + dir);
  });
});

