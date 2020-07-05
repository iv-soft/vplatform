@{
var entity_manager = get_service<IVySoft.VPlatform.TemplateService.Entity.IEntityManager>();

var pages = entity_manager.get_collection<webpack.page>("pages");
}
const autoprefixer = require('autoprefixer');
const CopyPlugin = require('copy-webpack-plugin');

module.exports = {
  entry: {
	'bundle': ['./index.scss', './index.js'],
	@foreach(var page in pages)
        {
	@:'@page.path/bundle': ['./@page.path/index.scss', './@page.path/index.js'],
	}
  },
  devServer: {
    port: 8080,
    historyApiFallback: {
      index: 'index.html'
    }
  },
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: '[path][name].css',
            },
          },
          {loader: 'extract-loader'},
          {loader: 'css-loader'},
          {
            loader: 'postcss-loader',
            options: {
              plugins: () => [autoprefixer()]
            }
          },
          {
            loader: 'sass-loader',
            options: {
              sassOptions: {
                includePaths: ['./node_modules'],
              }
            },
          }
        ],
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        query: {
          presets: ['@@babel/preset-env'],
        },
      }
    ],
  },
  plugins: [
    new CopyPlugin([
      { from: 'index.html', to: '.' },
      { from: 'assets', to: 'assets' },
	@foreach(var page in pages)
        {
		@: { from: '@page.path/index.html', to: '@page.path' },
	}
    ]),
  ],
};