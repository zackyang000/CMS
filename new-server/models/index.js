import article from './article/article';
import comment from './article/comment';
import category from './article/category';
import board from './board/board';
import tag from './article/tag';
import gallery from './gallery/gallery';
import user from './system/user';

module.exports = {
  article: require('./article/article')
  comment: require('./article/comment')
  category: require('./article/category')
  board: require('./board/board')
  tag: require('./article/tag')
  gallery: require('./gallery/gallery')
  user: require('./system/user')
}
