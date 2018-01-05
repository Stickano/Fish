<?php
    if (isset($_POST['okSearchById']))
        $controller->searchById();
?>
    
<form method="post">
    <input type="number" name="id" required>
    <input type="submit" name="okSearchById" value="Søg">
</form>

<?php
echo'<table>';
echo'<thead>';
    echo'<td>Id</td>';
    echo'<td>Navn</td>';
    echo'<td>Art</td>';
    echo'<td>Vægt</td>';
    echo'<td>Sted</td>';
    echo'<td>Uge</td>';
echo'</thead>';
echo'<tbody>';
foreach ($controller->getAllValues() as $key) {
    echo'<tr>';
    echo'<td>'.$key['id'].'</td>';
    echo'<td>'.$key['fisherName'].'</td>';
    echo'<td>'.$key['fishType'].'</td>';
    echo'<td>'.$key['weight'].'</td>';
    echo'<td>'.$key['place'].'</td>';
    echo'<td>'.$key['week'].'</td>';      
    echo'</tr>'; 
}
echo'</tbody>';
echo'</table>';
?>