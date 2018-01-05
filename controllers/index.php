<?php

class IndexController{

    private $sessions;
    private $curl;

    private $allValues;

    public function __construct(Session $sessions, Curl $curl){
        $this->curl     = $curl;
        $this->sessions = $sessions;
        self::setAllValues();
    }    

    private function setAllValues(){
        $this->allValues = $this->curl->curl("http://smellslikefish.azurewebsites.net/Service1.svc/db/read");
    }

    public function getAllValues(){
        return $this->allValues;
    }

    public function searchById(){
        $id = $_POST['id'];

    }

}

?>