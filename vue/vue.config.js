const CopyWebpackPlugin = require("copy-webpack-plugin");
module.exports = {
  devServer: {
    disableHostCheck: true
  },
  configureWebpack: config => {
    if (process.env.NODE_ENV === "production") {
      return {
        plugins: [
          new CopyWebpackPlugin([
            {
              from: "src/lib/abp.js",
              to: "dist"
            },
            {
              from: "src/assets",
              to: "assets"
            }
          ])
        ]
      };
    } else {
      return {
        plugins: [
          new CopyWebpackPlugin([
            {
              from: "src/lib/abp.js",
              to: "dist"
            },
            {
              from: "src/assets",
              to: "assets"
            }
          ])
        ]
      };
    }
  }
};
