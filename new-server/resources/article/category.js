import { Resource } from 'node-odata'
import model from '../../models'
import auth from '../auth'

module.exports = Resource('category', model.category)
.orderBy('date desc')
.post()
  .auth(auth.admin)
.put()
  .auth(auth.admin)
.delete()
  .auth(auth.admin)
