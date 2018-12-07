layui.config({
    base: '/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
}).use(['index', 'table'], function () {
    var $ = layui.$
        , admin = layui.admin
        , element = layui.element
        , table = layui.table;
    table.render({
        elem: '#LAY-Table'
        , url: '/button/Data'
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', width: 80, title: 'ID', sort: true }
            , { field: 'ButtonText', title: '按钮名称' }
            , { field: 'Sortnum', title: '排序' }
            , { field: 'iconCls', title: ' icon样式' }
            , { field: 'IconUrl', title: 'icon链接' }
            , { field: 'ButtonTag', title: '按钮code' }
            , { field: 'Remark', title: '备注' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-toolbar' }
        ]]
        , page: true
        , limit: 30
        , height: 'full-220'
        , text: '对不起，加载出现异常！'
    });

    table.on('tool(LAY-Table")', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            var id = obj.data.Id;
            active.Del(id);
        } else if (obj.event === 'edit') {
            var tr = $(obj.tr);
            var id = obj.data.Id;
            active.Edite(id);

        }
    });

    var active = {
        Add: function () {
            layer.open({
                type: 2,
                title: "添加按钮",
                area: ['50%', '50%'],
                content: "/button/add"
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-front-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        debugger
                        var field = data.field; //获取提交的字段
                        console.log(field);
                        //提交 Ajax 成功后，静态更新表格中的数据
                        //$.ajax({});
                        //table.reload('LAY-user-front-submit'); //数据刷新
                        $.ajax({
                            url: '/button/add',
                            type: "post",
                            data: field,
                            error: function () {
                            },
                            success: function (data) {
                                //console.log(data);
                                //BindDatatables(selid);
                                //initMenuTree();
                            }
                        });
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
            });
        }, Edite: function (id) {
            layer.open({
                type: 2,
                title: "添加按钮",
                area: ['50%', '50%'],
                content: "/button/add" + id
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-front-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);
                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        debugger
                        var field = data.field; //获取提交的字段
                        console.log(field);
                        //提交 Ajax 成功后，静态更新表格中的数据
                        //$.ajax({});
                        //table.reload('LAY-user-front-submit'); //数据刷新
                        $.ajax({
                            url: '/button/add',
                            type: "post",
                            data: field,
                            error: function () {
                            },
                            success: function (data) {
                                //console.log(data);
                                //BindDatatables(selid);
                                //initMenuTree();
                            }
                        });
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
            });
        }, Del: function (id) {
            layer.confirm('真的删除行么', function (index) {
                $.ajax({
                    url: '/button/Del/' + id,
                    type: "get",
                    error: function () {
                    },
                    success: function (data) {
                    }
                });
                layer.close(index);
            });
        }
    }

    $('#AddBtn').on('click', function () {
        active['Add'] ? active['Add'].call(this) : '';
    });
})


//layui.config({
//    base: '/src/layuiadmin/' //静态资源所在路径
//}).extend({
//    index: 'lib/index' //主入口模块
//}).use(['index', 'table'], function () {
//    var layer = layui.layer
//        , $ = layui.jquery
//        , table = layui.table;

//    var active = {
//        add: function () {
//        },
//        del: function (id) {

//        }
//    }

//    //管理员管理
//    table.render({
//        elem: '#LAY-Table"'
//        , url: '/button/Data'//'/src/layuiadmin/json/useradmin/mangadmin.js' //模拟接口
//        , cols: [[
//            { type: 'checkbox', fixed: 'left' }
//            , { field: 'Id', width: 80, title: 'ID', sort: true }
//            , { field: 'UserName', title: '登录名' }
//            , { field: 'TrueName', title: '真实姓名' }
//            , { field: 'Mobile', title: '手机号' }
//            , { field: 'QQ', title: 'QQ' }
//            , { field: 'Email', title: '邮箱' }
//            , { field: 'IsAdmin', title: '是否超管', templet: '#IsSuper' }
//            , { field: 'AddTime', title: '加入时间', sort: true }
//            , { field: 'IsDisabled', title: '审核状态', templet: '#buttonTplz', minWidth: 80, align: 'center' }
//            , { field: 'Remark', title: '备注' }
//            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#table-toolbar' }
//        ]]
//        , page: true
//        , limit: 30
//        , height: 'full-220'
//        , text: '对不起，加载出现异常！'
//    });
//    //监听表格排序问题
//    //table.on('sort(LAY-Table")', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
//    //    objls = obj;
//    //    field = obj.field;
//    //    order = obj.type;
//    //    //有些时候，你可能需要根据当前排序的字段，重新向服务端发送请求，从而实现服务端排序，如：
//    //    table.reload('LAY-user-back-manage', { //testTable是表格容器id
//    //        initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
//    //        , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
//    //            field: field //排序字段
//    //            , order: order //排序方式
//    //            , Conditions: Conditions//JSON.stringify(Arr)
//    //        }
//    //    });
//    //});
//    //监听工具条
//    //table.on('tool(LAY-Table")', function (obj) {
//    //    var data = obj.data;
//    //    if (obj.event === 'del') {
//    //        var id = obj.data.Id;
//    //        layer.confirm('真的删除行么', function (index) {
//    //            $.ajax({
//    //                url: '/user/Del/' + id,
//    //                type: "get",
//    //                error: function () {
//    //                },
//    //                success: function (data) {
//    //                }
//    //            });
//    //            obj.del();
//    //            layer.close(index);
//    //        });
//    //    } else if (obj.event === 'edit') {
//    //        var tr = $(obj.tr);
//    //        var id = obj.data.Id;
//    //        layer.open({
//    //            type: 2
//    //            , title: '编辑用户'
//    //            , content: '/user/add/' + id
//    //            , maxmin: true
//    //            , area: ['36%', '70%']
//    //            , btn: ['确定', '取消']
//    //            , yes: function (index, layero) {
//    //                var iframeWindow = window['layui-layer-iframe' + index]
//    //                    , submitID = 'LAY-user-front-submit'
//    //                    , submit = layero.find('iframe').contents().find('#' + submitID);

//    //                //监听提交
//    //                iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
//    //                    var field = data.field; //获取提交的字段
//    //                    //提交 Ajax 成功后，静态更新表格中的数据
//    //                    $.ajax({
//    //                        url: '/user/add',
//    //                        type: "post",
//    //                        data: field,
//    //                        error: function () {
//    //                        },
//    //                        success: function (data) {
//    //                        }
//    //                    });
//    //                    active.reload();
//    //                    table.reload('LAY-user-front-submit'); //数据刷新
//    //                    layer.close(index); //关闭弹层
//    //                });

//    //                submit.trigger('click');
//    //            }
//    //            , success: function (layero, index) {

//    //            }
//    //        });
//    //    }
//    //});
//});



