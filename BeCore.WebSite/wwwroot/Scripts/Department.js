
layui.config({
    base: '/src/layuiadmin/' //静态资源所在路径
}).extend({
    index: 'lib/index' //主入口模块
    , dtree: '{/}/src/layuiadmin/modules/dtree/dtree'//定义该组件模块名
}).use(['index', 'dtree'], function () {
    var $ = layui.$
        , admin = layui.admin
        , dtree = layui.dtree
        , element = layui.element;

    element.render('nav', 'component-nav');
    element.render('nav', 'component-nav-active');

    element.on('nav(component-nav-active)', function (elem) {
        layer.msg(elem.text());
    });
});
var selid = 0;
layui.use(['tree', 'layer', 'useradmin', 'index', 'form', 'table', 'dtree'], function () {
    var layer = layui.layer
        , dtree = layui.dtree
        , table = layui.table
        , $ = layui.jquery;

    var treeData;
    //基础参数
    var field = '', order = '', Conditions = '', objls;
    function getTreeData() {
        //写一个按钮树
        dtree.render({
            elem: "#DepartTree",  //绑定元素
            url: "/Department/DTreeData"  //异步接口
        });
        //树的出发事件
        dtree.on("node('DepartTree')", function (param) {
            $('#DepId').val(param.nodeId);
            TableReLoad();
        });
    }


    getTreeData();


    var tableIns = table.render({
        elem: '#Datatables'
        , url: '/Department/Data/'
        , toolbar: '#toolbarDemo'
        , cols: [[
            { field: 'Id', title: 'ID', width: 80, fixed: 'left', unresize: true, sort: true }
            , { field: 'UserName', title: '用户名', width: 120 }
            , { field: 'Email', title: '用户邮箱' }
            , { field: 'Mobile', title: '手机' }
            , { field: 'QQ', title: 'QQ' }


            //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#barDemo' }
        ]]
        , page: true
        , limit: 30
        , height: 'full-220'
        , text: '对不起，加载出现异常！'
    });

    //监听行工具事件
    table.on('tool(Datatables)', function (obj) {
        var data = obj.data;
        if (obj.event === 'del') {
            layer.confirm('真的删除行么', function (index) {
                DelInfo(data.id);
                layer.close(index);
            });
        } else if (obj.event === 'edit') {
            AddInfo(data.id);
        }
    });

    function TableReLoad() {

        var Arr = new Array();
        $('.layui-form-item').find('input').each(function () {
            var _Model = {};
            _Model.Key = $(this).attr('name');
            _Model.Value = $(this).val();
            _Model.OperatorMethod = $(this).attr('date-operator');
            if (_Model.Key != '')
                Arr.push(_Model);

        });
        //这里添加默认查询条件
        var _First = {};
        _First.Key = 'DepartmentId';
        _First.Value = parseInt($('#DepId').val());
        _First.OperatorMethod = 'Equal';
        Arr.push(_First);

        Conditions = JSON.stringify(Arr)

        table.reload('Datatables', { //testTable是表格容器id
            initSort: objls //记录初始排序，如果不设的话，将无法标记表头的排序状态。 layui 2.1.1 新增参数
            , where: { //请求参数（注意：这里面的参数可任意定义，并非下面固定的格式）
                field: field //排序字段
                , order: order //排序方式
                , Conditions: Conditions//JSON.stringify(Arr)
            }
        });
    }

    var active = {
        Add: function () {
            layer.open({
                type: 2,
                title: "添加部门",
                area: ['50%', '50%'],
                content: "/Department/add"
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-front-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID);

                    //监听提交
                    iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                        var field = data.field; //获取提交的字段
                        console.log(field);
                        //提交 Ajax 成功后，静态更新表格中的数据
                        //$.ajax({});
                        //table.reload('LAY-user-front-submit'); //数据刷新
                        $.ajax({
                            url: '/Department/add',
                            type: "post",
                            data: field,
                            error: function () {
                            },
                            success: function (data) {
                                //console.log(data);
                                //BindDatatables(selid);
                                initMenuTree();
                            }
                        });
                        layer.close(index); //关闭弹层
                    });

                    submit.trigger('click');
                }
            });
        },
        bindUser: function () {

        }
    }

    $('#DepartAdd').on('click', function () {
        active['Add'] ? active['Add'].call(this) : '';
    });

    $('#DepartDel').on('click', function () {
        var depid = $('#DepId').val();
        if (depid != '') {
            layer.confirm("你确定删除数据吗?如果存在下级节点则一并删除，此操作不能撤销！", { icon: 3, title: '提示' },
                function (index) {//确定回调
                    $.ajax({
                        url: '/Department/DelDep/' + depid,
                        error: function () {
                        },
                        success: function (data) {
                        }
                    });
                    layer.close(index);
                }, function (index) {//取消回调
                    layer.close(index);
                }
            );
        }
        else {
            layer.msg('请选择您要删除的部门');
        }

    })

    $('#DepartBind').on('click', function () {
        var depid = $('#DepId').val();
        if (depid != '') {
            layer.open({
                type: 2,
                title: "绑定用户",
                area: ["60%", "80%"],
                content: "/Department/bindUser"
                , btn: ['确定', '取消']
                , yes: function (index, layero) {
                    var iframeWindow = window['layui-layer-iframe' + index]
                        , submitID = 'LAY-user-front-submit'
                        , submit = layero.find('iframe').contents().find('#' + submitID)
                        , clcBtn = layero.find('iframe').contents().find('#GetSel')
                        , input = layero.find('iframe').contents().find('#selids');
                    clcBtn.click();
                    var ids = $(input).val();
                    $.ajax({
                        url: '/Department/SaveBindUser',
                        type: "post",
                        data: { id: depid, userids: ids },
                        error: function () {
                        },
                        success: function (data) {
                        }
                    });
                    layer.close(index); //关闭弹层

                }
            })
        }
        else {
            layer.msg('请选择要操作的部门');
        }

    })
});

//选择角色弹层
function AddCode(id) {
    if (id != 0)
        id = selid;
    if (id.toString().indexOf("Appli") > -1) {
        layer.msg('该节点不允许修改');
    } else {

        layer.open({
            //layer提供了5种层类型。可传入的值有：0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
            type: 2,
            title: "添加编码",
            area: ['50%', '50%'],
            content: '/Code/Add/' + id
            , btn: ['确定', '取消']
            , yes: function (index, layero) {
                var iframeWindow = window['layui-layer-iframe' + index]
                    , submitID = 'LAY-user-front-submit'
                    , submit = layero.find('iframe').contents().find('#' + submitID);

                //监听提交
                iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                    var field = data.field; //获取提交的字段
                    console.log(field);
                    //提交 Ajax 成功后，静态更新表格中的数据
                    //$.ajax({});
                    //table.reload('LAY-user-front-submit'); //数据刷新
                    $.ajax({
                        url: '/code/add',
                        type: "post",
                        data: field,
                        error: function () {
                        },
                        success: function (data) {
                            //console.log(data);
                            //BindDatatables(selid);
                            initMenuTree();
                        }
                    });
                    layer.close(index); //关闭弹层
                });

                submit.trigger('click');
            }
        });
    }
    //form.render()
}

function AddInfo(id) {
    layer.open({
        //layer提供了5种层类型。可传入的值有：0（信息框，默认）1（页面层）2（iframe层）3（加载层）4（tips层）
        type: 2,
        title: "添加编码",
        area: ['50%', '50%'],
        content: '/Code/Addinfo/' + id + '/?parentid=' + selid
        , btn: ['确定', '取消']
        , yes: function (index, layero) {
            var iframeWindow = window['layui-layer-iframe' + index]
                , submitID = 'LAY-user-front-submit'
                , submit = layero.find('iframe').contents().find('#' + submitID);

            //监听提交
            iframeWindow.layui.form.on('submit(' + submitID + ')', function (data) {
                var field = data.field; //获取提交的字段
                console.log(field);
                //提交 Ajax 成功后，静态更新表格中的数据
                //$.ajax({});
                //table.reload('LAY-user-front-submit'); //数据刷新
                $.ajax({
                    url: '/code/addinfo',
                    type: "post",
                    data: field,
                    error: function () {
                    },
                    success: function (data) {
                        //console.log(data);
                        BindDatatables(selid);
                    }
                });
                layer.close(index); //关闭弹层
            });

            submit.trigger('click');
        }
    });
}


function DelInfo(id) {
    $.ajax({
        url: '/code/DelInfo/' + id,
        //data: field,
        error: function () {
        },
        success: function (data) {
            //console.log(data);
            BindDatatables(selid);
        }
    });

}


function BindDatatables(id) {

    layui.use(['index', 'table'], function () {
        var table = layui.table;

        var tableIns = table.render({
            elem: '#Datatables'
            , url: '/Department/Data/'
            , toolbar: '#toolbarDemo'
            , cols: [[
                { field: 'id', title: 'ID', width: 80, fixed: 'left', unresize: true, sort: true }
                , { field: 'UserName', title: '用户名', width: 120 }
                , { field: 'Email', title: '用户邮箱' }
                , { field: 'Mobile', title: '手机' }
                , { field: 'QQ', title: 'QQ' }


                //, { title: '操作', width: 150, align: 'center', fixed: 'right', toolbar: '#barDemo' }
            ]]
            , page: true
            , limit: 30
            , height: 'full-220'
            , text: '对不起，加载出现异常！'
        });

        //监听行工具事件
        table.on('tool(Datatables)', function (obj) {
            var data = obj.data;
            if (obj.event === 'del') {
                layer.confirm('真的删除行么', function (index) {
                    DelInfo(data.id);
                    layer.close(index);
                });
            } else if (obj.event === 'edit') {
                AddInfo(data.id);
            }
        });

    });


}
