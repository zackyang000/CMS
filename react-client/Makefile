default: run

.PHONY: default

install:
	sudo npm install -g grunt-cli bower
	sudo npm install
	bower install

run: 
	grunt

deploy:
	NODE_ENV=production webpack -p --config webpack.production.config.js
	grunt --deploy

