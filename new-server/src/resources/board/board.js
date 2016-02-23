import { Resource } from 'node-odata'
import model from '../../models'
import auth from '../auth'

module.exports = Resource('board', model.board)
.orderBy('date desc')
.put()
  .auth(auth.admin)
.delete()
  .auth(auth.admin);
