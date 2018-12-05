from flask import jsonify, request, current_app, url_for
from . import api
from .. import db
from ..models import User
from ..decorator import login_required

@api.route('/topthree/', methods = ['GET'])
def topthree():
    def userinfofactory(user):
        return {
            "Email": user.email,
            "UserName": user.username,
            "Score": user.score 
        }

    users = User.query.all()
    sort(users, key = lambda u:u.score)
    if len(users) >= 3:
        users = users[:3]
    returninfos = []
    for u in users:
        returninfos.append(userinfofactory(u))
    return jsonify(returninfos)


@api.route('/challenge/', methods = ['GET'])
def challenge():
    myid = request.args.get('myid')
    email = request.args.get('email')
    score = request.args.get('score')
    user = User.query.filter_by(email = email).first() or None
    if not user:
        return jsonify({
                'Msg': 'notfound',
                'Code': 400
            })
    
    user.ifchallenge = 1
    user.challenger_id = myid
    return jsonify({
        "Msg": "ok",
        "Code": 200
        })

@api.route('/status/', methods = ['GET'])
def status():
    myid = request.args.get('myid')
    user = User.query.filter_by(id = myid).first() or None
    if not user:
        return jsonify({
                'Msg': 'user not found',
                'Code': '400'
            })

    if user.ifchallenge:
        cu = User.query.filter_by(id = user.challenger_id).first() or None
        return jsonify({
                'Msg': 'yes',
                'Score': cu.score,
                'Code': 200
            })
    else :
        return jsonify({
                'Msg': 'no',
                'Code': 404
            })


@api.route('/score/', methods = ['GET'])
def score():
    userid = request.get_json().get("UserId")
    score = request.get_json().get("Score")
    user = User.query.filter_by(id = userid).first() or None
    if not user:
        return jsonify({
                'Msg': 'user not found',
                'Code': 400
            })
    user.score = score
    return jsonify({
            'Msg': 'ok',
            'Code': 200
        })
    
