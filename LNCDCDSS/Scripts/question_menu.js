
$(document).ready(function () {
    // debugger;
    $("#mainmenu").find(".pre").hide(); //��ʼ��Ϊ��һ��
    var page = 1; //��ʼ����ǰ�İ���Ϊ1
    var $show = $("#mainmenu").find(".menubox"); //�ҵ�ͼƬչʾ����
    var page_count = $show.find("ul.qeslist").length;
    var $width_box = $show.parents("#querybox").width(); //�ҵ�ͼƬչʾ������Χ��div
    //alert(page_count);
    //��ʾtitle����
    //		$show.find("li").hover(function(){
    //			$(this).find(".title").show();								
    //		},function(){
    //			$(this).find(".title").hide();
    //		})

    function nav() {
        if (page == 1) {
            $("#mainmenu").find(".pre").hide().siblings(".next").show(); //siblings(selected)���ƥ�伯����ÿ��Ԫ�ص�ͬ��
        } else if (page == page_count) {
            $("#mainmenu").find(".next").hide().siblings(".pre").show();
        } else {
            $("#mainmenu").find(".pre").show().siblings(".next").show();
        }
    }

    $("#mainmenu").find(".next").click(function () {

        if (!$show.is(":animated")) {
            $show.animate({ left: '-=' + $width_box }, "normal");
            page++;
            nav();
            $number = page - 1;
            $("#mainmenu").find(".nav a:eq(" + $number + ")").addClass("now").siblings("a").removeClass("now");
            var mmseval = ($("input[name='step']:checked").length);
            $("#SAD1").attr("value", mmseval);
            //�ʱ�ѧϰ
            var vl1 = $("#time1 input:checked").length;
            $("#SAD3").attr("value", vl1);
            var vl2 = $("#time2 input:checked").length;
            $("#SAD4").attr("value", vl2);
            var vl3 = $("#time3 input:checked").length;
            $("#SAD5").attr("value", vl3);
            //ͼ�μ���
            var pmall1 = 0;
            var pmall2 = 0;
            var pmall3 = 0;
            $.each($('.C3A select option:selected'), function () {
                pm = $(this).text();
                if (pm == "")
                { pm = "0"; }
                pmint = parseInt(pm);
                pmall1 += pmint;
            });
            $("#PM1").attr("value", pmall1);
            $("#SAD61").attr("value", pmall1);
            $.each($('.C3B select option:selected'), function () {
                pm = $(this).text();
                if (pm == "")
                { pm = "0"; }
                pmint = parseInt(pm);
                pmall2 += pmint;
            });
            $("#PM2").attr("value", pmall2);
            $("#SAD62").attr("value", pmall2);
            $.each($('.C3C select option:selected'), function () {
                pm = $(this).text();
                if (pm == "")
                { pm = "0"; }
                pmint = parseInt(pm);
                pmall3 += pmint;
            });
            $("#PM3").attr("value", pmall3);
            $("#SAD63").attr("value", pmall3);

            //IADL
            var IADL = 0;
            var count = 0;
            $.each($('#iadl select option:selected'), function () {
                pm = $(this).val();
                count = parseInt(pm);
                IADL += count;
            });
            $("#SAD2").attr("value", IADL);

            //�ӿռ���ִ��������ע�⡢����������Ľ��
            if ($("#mt1").prop("checked"))
            { $("#SAD7").attr("value", "1"); }
            else $("#SAD7").attr("value", "0");
            if ($("#mt2").prop("checked"))
            { $("#SAD8").attr("value", "1"); }
            else $("#SAD8").attr("value", "0");
            var mt3 = ($("input[name='clock']:checked").length);
            $("#SAD9").attr("value", mt3);
            var mt4 = ($("input[name='animal']:checked").length);
            $("#SAD10").attr("value", mt4);
            var mt5 = ($("input[name='columns']:checked").length);
            $("#SAD11").attr("value", mt5);
            if ($("#mt6").prop("checked"))
            { $("#SAD12").attr("value", "1"); }
            else $("#SAD12").attr("value", "0");
            //ֻ�ܵ�ѡ
            var mt7 = 0;
            if ($("#calculate1").prop("checked"))
            { mt7 = 1; }
            else if ($("#calculate2").prop("checked"))
            { mt7 = 2; }
            else if ($("#calculate3").prop("checked"))
            { mt7 = 3; }
            else { mt7 = 0; }
            $("#SAD13").attr("value", mt7);

            return false;
        };
    })


    $("#mainmenu").find(".pre").click(function () {
        if (!$show.is(":animated")) {
            $show.animate({ left: '+=' + $width_box }, "normal");
            page--;
            nav();
            $number = page - 1;
            $("#mainmenu").find(".nav a:eq(" + $number + ")").addClass("now").siblings("a").removeClass("now");
        }
        return false;
    })

    $("#mainmenu").find(".nav a").click(function () {
        $index = $(this).index();
        page = $index + 1;
        nav();
        $show.animate({ left: -($width_box * $index) }, "normal");
        $(this).addClass("now").siblings("a").removeClass("now");
        return false;
    })
});
