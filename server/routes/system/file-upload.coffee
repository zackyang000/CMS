fs = require("fs")

module.exports = (app, prefix) ->
  app.post prefix + "/file-upload", (req, res) ->
    # 获得文件的临时路径
    tmp_path = req.files.file.path

    # 指定文件上传后的目录 - 示例为"images"目录。
    target_path = "../client/upload/" + req.files.file.name
    debugger

    # 移动文件
    fs.rename tmp_path, target_path, (err) ->
      throw err  if err
      debugger

      # 删除临时文件夹文件,
      fs.unlink tmp_path, ->
        throw err  if err
        res.send "File uploaded to: " + target_path + " - " + req.files.file.size + " bytes"
