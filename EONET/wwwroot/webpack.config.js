const path = require('path');

module.exports = {
    entry: {
        'bundle': './index.js'
    },
	resolve: {
		extensions: ['*', '.js', '.jsx']
    },
    mode: 'development',
    devtool: 'inline-source-map',
    output: {
        path: path.resolve(__dirname, 'js'),
        publicPath: 'js/',
        filename: '[name].js'
    },
	module: {
		rules: [
			{
				test: /\.jsx?$/,
				exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-react', '@babel/preset-env'],
                        plugins: ['@babel/plugin-proposal-class-properties','@babel/plugin-proposal-nullish-coalescing-operator']
                    }
                }
            },
            {
                test: /\.css$/,
                use: ['style-loader', 'css-loader'],
            }
		]
    }
};