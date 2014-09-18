###
Operator  Description             Example
Comparison Operators
eq        Equal                   Address/City eq 'Redmond'
ne        Not equal               Address/City ne 'London'
gt        Greater than            Price gt 20
ge        Greater than or equal   Price ge 10
lt        Less than               Price lt 20
le        Less than or equal      Price le 100
has       Has flags               Style has Sales.Color'Yellow'    #todo
Logical Operators
and       Logical and             Price le 200 and Price gt 3.5
or        Logical or              Price le 3.5 or Price gt 200     #todo
not       Logical negation        not endswith(Description,'milk') #todo

eg.
  http://host/service/Products?$filter=Price lt 10.00
  http://host/service/Categories?$filter=Products/$count lt 10
###

module.exports = (query, $filter) ->
  return unless $filter

  for item in $filter.split('and')
    condition = item.split(' ').filter (n)->n
    if condition.length != 3
      throw new Error("Syntax error at '#{item}'.")
    [key, odataOperator, value] = condition
    operatorMap =
      #odata : mangoose
      'eq' : 'equals'
      'ne' : 'ne'
      'gt' : 'gt'
      'ge' : 'gte'
      'lt' : 'lt'
      'le' : 'lte'
    mongoOperator = operatorMap[odataOperator]
    unless mongoOperator
      throw new Error("Incorrect operator at '#{item}'.")
    query.where(key)[mongoOperator](value)
    #todo 查询value中包含空格的问题 需要使用单引号