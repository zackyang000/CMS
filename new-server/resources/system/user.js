import { Resource, resources } from 'node-odata'
import model from '../../models'
import auth from '../auth'

module.exports = Resource('user', model.user)
.orderBy('date desc')
.list()
  .auth(auth.admin)
.get()
  .auth(auth.admin)
.post()
  .auth(auth.admin)
.delete()
  .auth(auth.admin)
.put()
  .auth(auth.admin)
  .after((newEntity, oldEntity) => {
    resources.article.find( {'meta.author': oldEntity.name }).exec((err, articles) => {
      articles.map((article) => {
        article.meta.author = newEntity.name;
        article.save();
      });
    });
  });

