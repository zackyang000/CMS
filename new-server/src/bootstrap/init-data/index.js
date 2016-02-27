import { resources } from 'node-odata';

const initData = (model, path) => {
  require(path).forEach((item) => {
    const data = new model(item);
    data.save();
    console.log(`data init: ${path} import successful.`);
  });
};

module.exports = {
  import: () => {
    resources.user.find().exec((err, users) => {
      if (!users.length) {
        initData(resources.user, './system/user.json');
        initData(resources.article, './article/article.json');
        initData(resources.category, './article/category.json');
        initData(resources.board, './board/board.json');
        initData(resources.gallery, './photo/gallery.json');
      }
    });
  },
};
