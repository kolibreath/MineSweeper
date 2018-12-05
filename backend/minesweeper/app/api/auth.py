from flask import jsonify, request, current_app, url_for
from . import api
from .. import db
from ..models import User
from ..decorator import login_required

@api.route('/signup/', methods = ['POST'])
def signup():
    username = request.get_json().get('UserName')
    email = request.get_json().get('Email')
    password = request.get_json().get('Password')
    user = User.query.filter_by(email = email).first() or None
    if user:
        response = jsonify({
            "Msg":"user exist.",
            "Code": 400
            })
        return response 
    
    u = User(
            username = username,
            email = email,
            score = 0,
            password = password,
            ifchallenge = 0,
            challenger_id = -1
        )
    db.session.add(u)
    db.session.commit()
    return jsonify({
        "Msg": "ok",
        "Code": 200
        })


@api.route('/login/', methods = ['POST'])
def login():
    email = request.get_json().get('Email')
    password = request.get_json().get('Password')
    user = User.query.filter_by(email = email).first() or None
    if not user:
        return jsonify({
            "Msg": "user not exist.",
            "Code": 400
            })
    if not user.verify_password(password):
        return jsonify({
            "Msg": "password error",
            "Code": 400
            })
    return jsonify({
        "Msg": "ok",
        "UserId": user.id,
        "Code": 200
        })
