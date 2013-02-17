function showform(id)
{
	var i;
	//whichEl = document.getElementById("de" + id);
	document.getElementById("tag" + id ).className= "lock";
	
	for(i=1;i<=3;i++)		
		if (i!=id)
			document.getElementById("tag" + i ).className="";

}