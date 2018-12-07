var field = '', order = '', Conditions = '', objls;


layui.config({
    base: '/src/layuiadmin/' //静态资源所在路径
}).extend({
    dtree: '{/}/src/layuiadmin/modules/dtree/dtree'//定义该组件模块名
    , treeGrid: 'treeGrid'
    , index: 'lib/index' //主入口模块
    //, treeSelect: 'modules/treeSelect'
}).use(['index', 'treeGrid', 'form', 'dtree', 'table'], function () {
    //layui.use(['tree', 'layer', 'useradmin', 'index', 'form', 'table']
    var $ = layui.$
        , dtree = layui.dtree
        , admin = layui.admin
        , element = layui.element
        , table = layui.table;
    var active = {
        reload: function () {
            var Arr = new Array();
            $('.layui-form-item').find('input').each(function () {
                var _Model = {};
                _Model.Key = $(this).attr('name');
                _Model.Value = $(this).val();
                _Model.OperatorMethod = $(this).attr('date-operator');
                if (_Model.Key != '')
                    Arr.push(_Model);
            });
            console.log('Arr:' + typeof (Arr));
            Conditions = JSON.stringify(Arr)

            table.reload('LAY-user-back-manage', { //testTable是表格容器id
                initSort: objls //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
                , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                    field: field //排序字段
                    , order: order //排序方式
                    , Conditions: Conditions//JSON.stringify(Arr)
                }
            });
        },
        add: function () {
            layer.open({
                //layer提供了5种层类型。可传入的值有：0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
                type: 2,
                title: "添加角色",
                area: ['36%', '70%'],
                content: '/Role/Add/'
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
                        //table.reload('LAY-user-front-submit'); //数据刷新
                        console.log(field);
                        $.ajax({
                            url: '/Role/add',
                            type: "post",
                            data: field,
                            error: function () {
                            },
                            success: function (data) {
                                layer.close(index); //关闭弹层

                            }
                        });
                        //active.reload();
                    });

                    submit.trigger('click');
                    TableReload();
                }
            });
        },
        BindNav: function (id) {
            layer.open({
                type: 1, //type:0 也行
                title: "选择权限",
                area: ["400px", "80%"],
                content: '<ul id="openTree3" class="dtree" data-id="0"></ul>',
                btn: ['确认选择'],
                success: function (layero, index) {
                    var DTree = dtree.render({
                        obj: $(layero).find("#openTree3"),
                        url: "/Data/RoleNav",
                        async: false,//同步加载
                        checkbar: true // 开启复选框
                    });
                },
                yes: function (index, layero) {
                    var flag = true;
                    var params = dtree.getCheckbarNodesParam("openTree3"); // 获取选中值
                    if (params.length == 0) {
                        layer.msg("请至少选择一个节点", { icon: 2 });
                        flag = false;
                    }

                    var ids = [], names = [];
                    for (var key in params) {
                        var param = params[key];
                        ids.push(param.nodeId);
                        names.push(param.context);
                    }


                    var selid = $('#methodid').val();

                    layer.close(index); //关闭弹层
                    if (flag) {
                        $.ajax({
                            url: '/Bind/Save',
                            type: "post",
                            data: { id: selid, attrid: ids.join(",") },
                            error: function () {
                            },
                            success: function (data) {
                                table.reload('Datatables', { //testTable是表格容器id
                                    //initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
                                    where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                                        //field: field //排序字段
                                        //, order: order //排序方式
                                        //, Conditions: Conditions//JSON.stringify(Arr)
                                        id: selid
                                    }
                                });
                            }
                        });
                        layer.close(index);
                    }
                }
            });
        }
    }
    //管理员管理
    table.render({
        elem: '#LAY-user-back-manage'
        , url: '/Role/Data'//'/src/layuiadmin/json/useradmin/mangadmin.js' //模拟接口
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'Id', width: 80, title: 'ID', sort: true }
            , { field: 'RoleName', title: '角色名称' }
            , { field: 'Sortnum', title: '排序' }
            , { field: 'IsDefault', title: '是否默认' }
            , { field: 'Remark', title: '备注' }
            , { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#barDemo' }
        ]]
        , page: true
        , limit: 30
        , height: 'full-220'
        , text: '对不起，加载出现异常！'
    });
    //监听表格排序问题
    table.on('sort(LAY-user-back-manage)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
        objls = obj;
        field = obj.field;
        order = obj.type;
        //有些时候，你可能需要根据当前排序的字段，重新向服务端发送请求，从而实现服务端排序，如：
        table.reload('LAY-user-back-manage', { //testTable是表格容器id
            initSort: obj //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                field: field //排序字段
                , order: order //排序方式
                , Conditions: Conditions//JSON.stringify(Arr)
            }
        });
    });
    function TableReload() {
        table.reload('LAY-user-back-manage', { //testTable是表格容器id
            initSort: objls //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                field: field //排序字段
                , order: order //排序方式
                , Conditions: Conditions//JSON.stringify(Arr)
            }
        });
    }
    //监听工具条
    table.on('tool(LAY-user-back-manage)', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            var id = obj.data.Id;
            layer.confirm('真的删除行么', function (index) {
                $.ajax({
                    url: '/role/Del/' + id,
                    type: "get",
                    error: function () {
                    },
                    success: function (data) {
                        TableReload();
                    }
                });
                obj.del();
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            var tr = $(obj.tr);
            var id = obj.data.Id;
            layer.open({
                type: 2
                , title: '编辑用户'
                , content: '/role/add/' + id
                , maxmin: true
                , area: ['36%', '70%']
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-front-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        //提交 Ajax 成功后，静态更新表格中的数据
                        $.ajax({
                            url: '/user/add',
                            type: "post",
                            data: field,
                            error: function () {
                            },
                            success: function (data) {
                                TableReload();
                            }
                        });
                        //active.reload();
                        //table.reload('LAY-user-front-submit'); //数据刷新
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
                , success: function (layero, index) {

                }
            });
        }
    });
    //按钮监听
    $('#LAY-user-back-search').on('click', function () {
        active['reload'] ? active['reload'].call(this) : '';
    });

    $('#useradd').on('click', function () {
        active['add'] ? active['add'].call(this) : '';
    });
    $('#addMenu').on('click', function () {
        //active['BindNav'] ? active['BindNav'].call(this) : '';
        active.BindNav(0);
    });
});
