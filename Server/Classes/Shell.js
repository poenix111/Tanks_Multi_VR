var ServerObject = require('./ServeObject.js');
var Vector3 = require('./Vector3.js');

module.exports = class Shell extends ServerObject {
    constructor() {
        super();
        this.direction = new Vector3();
        this.speed = 0.5;
        this.gravity = 9.8/1000;
    }

    onUpdate() {

        this.position.x += this.direction.x * this.speed;
        this.position.y += this.direction.y + this.speed;x      
        this.position.z += this.direction.z * this.speed;

        return (this.y <= 0)? true: false;
    }
}