#!/usr/bin/wish


proc filllist {} {
    global flist hidden path

    if {$hidden} {
        set files [glob -directory ${path} -tail {{.[a-zA-Z0-9],}*}]
    } else {
        set files [glob -directory ${path} -tail *]
    }


    set files [lsort -dictionary $files]

    $flist delete 0 end
    foreach w $files {
        $flist insert end $w
    }
}

proc selectall {} {
	global flist

	$flist selection set 0 [$flist size]
}

proc fileext {str} {
    return [string range $str [expr [string last . $str] + 1] [string length $str]]
}

proc dorename {} {
    global flist template
    
    set sels [$flist curselection]
    set temp [$template get]

    if {[expr [llength $sels] <= 0]} {
        tk_messageBox -title "Error" -message "File selection empty" -parent . -type ok -icon error
        return
    } elseif {[string equal $temp ""]} {
        tk_messageBox -title "Error" -message "Template empty" -parent . -type ok -icon error
        return
    }

    set count 1
    foreach ifile $sels {
        set ffile [$flist get $ifile]
        set ext [fileext $ffile]        
    
        set newname [format $temp $count]
        set newname "$newname.$ext"

        file rename $ffile $newname

        set count [expr $count + 1]
    }
    filllist
}

proc lreverse {lst} {

	set size [llength $lst]
	for {set count 0} {$count < $size} {incr count} {
		lappend l [lindex $lst [expr $size - $count - 1]]
	}
	return $l
}

proc moveup {} {
    global flist

    set sels [$flist curselection]

    foreach sel $sels {
        if {[expr $sel != 0]} {
            set val [$flist get $sel]
            $flist del $sel
            $flist insert [expr $sel - 1] $val
            lset sels [lsearch $sels $sel] [expr $sel - 1]
        }
    }

    $flist selection clear 0 [$flist size]

    foreach sel $sels {
        $flist selection set $sel
    }
}

proc movedown {} {
    global flist

    set sels [lreverse [$flist curselection]]

    foreach sel $sels {
        if {[expr $sel != [expr [$flist size] - 1]]} {
            set val [$flist get $sel]
            $flist del $sel
            $flist insert [expr $sel + 1] $val
            lset sels [lsearch $sels $sel] [expr $sel + 1]
        }
    }

    $flist selection clear 0 [$flist size]

    foreach sel $sels {
        $flist selection set $sel
    }
}

proc genbuttonframe {gf3} {
    upvar $gf3 f3

    set f3 [frame .f3]

    grid [checkbutton $f3.c1 -text "Hidden files" -command togglehidden] \
        -column 0 -row 0 -sticky w -padx 5
    grid [button $f3.c2 -text "Up" -command moveup] \
        -column 1 -row 0 -sticky w -padx 5
    grid [button $f3.c3 -text "Down" -command movedown] \
        -column 2 -row 0 -sticky w -padx 5
    grid [button $f3.c4 -text "Refresh" -command filllist] \
        -column 3 -row 0 -sticky w -padx 5
    grid [button $f3.c5 -text "All" -command selectall] \
        -column 4 -row 0 -sticky w -padx 5
}

proc genframe1 {gf1 gflist} {
    upvar $gf1 f1
    upvar $gflist flist 

    set f1 [frame .f1]
    set s1 [scrollbar $f1.s1]
    set flist [listbox $f1.flist -selectmode extended]
    $flist config -yscrollcommand "$s1 set"
    $s1 config -command "$flist yview"
    grid $flist -column 0 -row 0 -sticky news -padx 5 -pady 5
    grid $s1 -column 1 -row 0 -sticky news -pady 5

    genbuttonframe f3
    grid $f3 -column 0 -row 1 -sticky news -padx 5

    grid columnconfig $f1 0 -weight 1
    grid columnconfig $f1 1 -weight 0
    grid rowconfig $f1 0 -weight 1
    grid rowconfig $f1 1 -weight 0

}

proc genframe2 {gf2 gtemplate} {
    upvar $gf2 f2
    upvar $gtemplate template

    set f2 [frame .f2]
    pack [label $f2.l1 -text "Template:"] -side top -anchor w -padx 5
    set template [entry $f2.template]
    pack $template -side top -fill x -padx 5
    pack [label $f2.l2 -text "Old extension will be concatenated automatically!\n%d will be replaced with position\njust like printf...\n" \
        -relief ridge -justify left] -side top -fill x -pady 10 -padx 5
    set f3 [frame $f2.f3]
    pack [button $f3.b1 -text "Quit" -command exit] -side right -pady 5
    pack [button $f3.b2 -text "Rename" -command dorename] -side right -pady 5
    pack [button $f3.b3 -text "Add Placeholder" -command addplaceholder] -side right -pady 5
    pack $f3 -side top -anchor e
}

proc addplaceholder {} {
	global template

	$template insert end "%02d"
}
proc togglehidden {} {
    global hidden
    set hidden [expr !$hidden]
    filllist
}

proc makegui {gflist gtemplate} {
    upvar $gflist flist
    upvar $gtemplate template 

    genframe1 f1 flist
    genframe2 f2 template

    grid $f1 -column 0 -row 0 -sticky news 
    grid $f2 -column 1 -row 0 -sticky news 
    grid columnconfig . 0 -weight 1
    grid rowconfig . 0 -weight 1
    grid columnconfig . 1 -weight 1

}

if {[expr $argc > 0]} {
    set path [lindex $argv 0]
} else {
    set path ""
}
set hidden false
makegui flist template
filllist


